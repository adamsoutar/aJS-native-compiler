using System;
using System.Diagnostics;
using Esprima;
using Esprima.Ast;

namespace ajs_codegen
{
    public class Codegen
    {
        string Emitted;

        public string EmitCodeForJS(string jsCode)
        {
            // We use a class-scoped var so we don't have
            // to pass the string around
            Emitted = "";

            EmitSkeletonStartup();

            var parser = new JavaScriptParser(jsCode);
            var program = parser.ParseScript();
            EmitForASTNode(program);

            EmitSkeletonShutdown();

            return Emitted;
        }

        void EmitSkeletonStartup()
        {
            Emitted += @"
using System;
using ajs_lib;
using ajs_lib.StdLib;

namespace ajs_test
{
    class Program
    {
        static Global jsGlobal;

        static void Main(string[] args)
        {
            jsGlobal = new Global();

            // Similar to the local this object
            var scope = new JSObject(jsGlobal);
";
        }

        void EmitSkeletonShutdown ()
        {
            Emitted += @"
        }
    }
}";
        }

        void EmitForASTNode (INode node)
        {
            switch (node.Type)
            {
                case Nodes.Program:
                    EmitForProgram((Program)node);
                    break;
                case Nodes.VariableDeclaration:
                    EmitForVariableDeclaration((VariableDeclaration)node);
                    break;
                case Nodes.ExpressionStatement:
                    EmitForExpressionStatement((ExpressionStatement)node);
                    break;
                case Nodes.FunctionDeclaration:
                    EmitForFunctionDeclaration((FunctionDeclaration)node);
                    break;
                case Nodes.BlockStatement:
                    EmitForBlockStatement((BlockStatement)node);
                    break;
                default:
                    throw new NotImplementedException($"Unimplemented node {node}");
            }
            Emitted += "\n";
        }

        void EmitForProgram (Program prog)
        {
            foreach (var node in prog.ChildNodes)
            {
                EmitForASTNode(node);
            }
        }

        void EmitForBlockStatement (BlockStatement block)
        {
            foreach (var node in block.ChildNodes)
            {
                EmitForASTNode(node);
            }
        }

        void EmitForFunctionDeclaration (FunctionDeclaration decl)
        {
            // TODO: Hoisting (without a preprocessing step. aJS _is_ a preprocessing step)

            if (decl.Params.Count > 0)
            {
                throw new NotImplementedException("User functions with arguments aren't implemented");
            }

            Emitted += $"scope.SetKey(\"{decl.Id.Name}\", new JSNativeFunction(args => {{\n";
            EmitForASTNode(decl.Body);

            // This is temp because return values aren't supported yet
            Emitted += "return new JSUndefined();";

            Emitted += "}));";
        }

        void EmitForCallExpression (CallExpression call)
        {
            Emitted += "(";
            EmitForExpression(call.Callee);
            Emitted += ").Call(new IJSObject[] {";
            foreach (var arg in call.Arguments)
            {
                EmitForExpression((Expression)arg);
                Emitted += ", ";
            }
            Emitted += "})";
        }

        void EmitForExpressionStatement (ExpressionStatement expStmt)
        {
            EmitForExpression(expStmt.Expression);
            Emitted += ";";
        }

        void EmitForVariableDeclaration (VariableDeclaration decl)
        {
            foreach (var dec in decl.Declarations)
            {
                EmitForVariableDeclarator(dec);
            }
        }

        void EmitForVariableDeclarator (VariableDeclarator dec)
        {
            Debug.Assert(dec.Id.Type == Nodes.Identifier);

            var ident = (Identifier)dec.Id;

            Emitted += $"scope.SetKey(\"{ident.Name}\", ";
            EmitForExpression(dec.Init);
            // TODO: Is it const? Hardcoded as no for now (all declarations are "let")
            Emitted += ");";
        }

        void EmitForExpression (Expression exp, bool identsAsStrings = false)
        {
            switch (exp.Type)
            {
                case Nodes.Literal:
                    EmitForLiteral((Literal)exp);
                    break;
                case Nodes.CallExpression:
                    EmitForCallExpression((CallExpression)exp);
                    break;
                case Nodes.MemberExpression:
                    EmitForMemberExpression((MemberExpression)exp);
                    break;
                case Nodes.Identifier:
                    EmitForIdentifier((Identifier)exp, identsAsStrings);
                    break;
                case Nodes.AssignmentExpression:
                    EmitForAssignmentExpression((AssignmentExpression)exp);
                    break;
                case Nodes.BinaryExpression:
                    EmitForBinaryExpression((BinaryExpression)exp);
                    break;
                default:
                    throw new NotImplementedException($"Unimplemented expression {exp}");
            }
        }

        void EmitForBinaryExpression (BinaryExpression bin)
        {
            string funcName;
            switch (bin.Operator)
            {
                case BinaryOperator.Plus:
                    funcName = "Plus";
                    break;
                default:
                    throw new NotImplementedException($"Unimplemented binary operator {bin.Operator}");
            }

            Emitted += $"Operators.{funcName}(";
            EmitForExpression(bin.Left);
            Emitted += ", ";
            EmitForExpression(bin.Right);
            Emitted += ")";
        }

        void EmitForAssignmentExpression (AssignmentExpression ass /* :) */)
        {
            if (ass.Operator != AssignmentOperator.Assign)
            {
                throw new NotImplementedException($"Unimplemented assignment operator {ass.Operator}");
            }

            // TODO: This doesn't work outside of an ExpressionStatement yet
            // TODO: Non-identifier LHS. This will be quite the job.

            Debug.Assert(ass.Left.Type == Nodes.Identifier);

            var ident = (Identifier)ass.Left;
            Emitted += $"scope.SetKey(\"{ident.Name}\", ";
            EmitForExpression(ass.Right);
            Emitted += ")";
        }

        void EmitForIdentifier (Identifier ident, bool identsAsStrings = false)
        {
            if (identsAsStrings)
            {
                // For member expressions
                Emitted += $"new JSString(\"{ident.Name}\")";
                return;
            }
            // TODO: Read from localThis
            Emitted += $"scope.GetKey(\"{ident.Name}\")";
        }

        void EmitForMemberExpression (MemberExpression exp)
        {
            Emitted += "(";
            EmitForExpression(exp.Object);
            Emitted += ").GetKey((";
            EmitForExpression(exp.Property, true);
            Emitted += ").AsCSharpString)";
        }

        void EmitForLiteral (Literal lit)
        {
            switch (lit.TokenType)
            {
                case TokenType.StringLiteral:
                    Emitted += $"new JSString(\"{lit.StringValue}\")";
                    return;
                case TokenType.NumericLiteral:
                    Emitted += $"new JSNumber({lit.NumericValue})";
                    return;
                default:
                    throw new NotImplementedException($"Unimplemented literal {lit}");
            }
        }
    }
}
