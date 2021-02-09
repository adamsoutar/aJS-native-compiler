using System;
using System.Diagnostics;
using Esprima;

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
";
        }

        void EmitSkeletonShutdown ()
        {
            Emitted += @"
        }
    }
}";
        }

        void EmitForASTNode (Esprima.Ast.INode node)
        {
            switch (node.Type)
            {
                case Esprima.Ast.Nodes.Program:
                    EmitForProgram((Esprima.Ast.Program)node);
                    break;
                case Esprima.Ast.Nodes.VariableDeclaration:
                    EmitForVariableDeclaration((Esprima.Ast.VariableDeclaration)node);
                    break;
                case Esprima.Ast.Nodes.ExpressionStatement:
                    EmitForExpressionStatement((Esprima.Ast.ExpressionStatement)node);
                    break;
                default:
                    throw new NotImplementedException($"Unimplemented node {node}");
            }
        }

        void EmitForProgram (Esprima.Ast.Program prog)
        {
            foreach (var node in prog.ChildNodes)
            {
                EmitForASTNode(node);
            }
        }

        void EmitForExpressionStatement (Esprima.Ast.ExpressionStatement expStmt)
        {
            EmitForExpression(expStmt.Expression);
        }

        void EmitForVariableDeclaration (Esprima.Ast.VariableDeclaration decl)
        {
            foreach (var dec in decl.Declarations)
            {
                EmitForVariableDeclarator(dec);
            }
        }

        void EmitForVariableDeclarator (Esprima.Ast.VariableDeclarator dec)
        {
            Debug.Assert(dec.Id.Type == Esprima.Ast.Nodes.Identifier);

            var ident = (Esprima.Ast.Identifier)dec.Id;

            Emitted += $"jsGlobal.SetKey(\"{ident.Name}\", new JSVariable(";
            EmitForExpression(dec.Init);
            // TODO: Is it const? Hardcoded as no for now (all declarations are "let")
            Emitted += ", false);";
        }

        void EmitForExpression (Esprima.Ast.Expression exp)
        {
            switch (exp.Type)
            {
                case Esprima.Ast.Nodes.Literal:
                    EmitForLiteral((Esprima.Ast.Literal)exp);
                    break;
                default:
                    throw new NotImplementedException($"Unimplemented expression {exp}");
            }
        }

        void EmitForLiteral (Esprima.Ast.Literal lit)
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
