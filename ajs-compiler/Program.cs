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
function sayHello () {
  console.log('Hello from a function!')
}
sayHello()
");

            Console.WriteLine(cSOutput);
        }
    }
}
