using System;
using ajs_lib;
using ajs_codegen;

namespace ajs_compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new JSNumber(1);
            //var b = new JSString("Hello");

            //Console.WriteLine(Operators.Plus(a, b).AsCSharpString);

            //var globalThis = new ajs_lib.StdLib.Global();

            //globalThis.GetKey("console").GetKey("log").Call(new IJSObject[] { new JSString("Hello, world!") });

            var codegen = new Codegen();
            var cSOutput = codegen.EmitCodeForJS("const x = 1; x = 2");

            Console.WriteLine(cSOutput);
        }
    }
}
