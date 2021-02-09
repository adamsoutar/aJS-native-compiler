using System;
using ajs_lib;
using ajs_codegen;

namespace ajs_compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var codegen = new Codegen();
            var cSOutput = codegen.EmitCodeForJS(@"
let x = 1

if (x === 1) {
  console.log('x is one')
}
");

            Console.WriteLine(cSOutput);
        }
    }
}
