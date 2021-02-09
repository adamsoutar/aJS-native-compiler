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
let n = 1
function runNextFizzbuzz () {
  let str = ''
  if (n % 3 === 0) {
    str = 'Fizz'
  }
  if (n % 5 === 0) {
    str = str + 'Buzz'
  }
  if (str === '') {
    str = n
  }
  console.log(str)
  n = n + 1
  if (n !== 101) {
    runNextFizzbuzz()
  }
}
runNextFizzbuzz()
");

            Console.WriteLine(cSOutput);
        }
    }
}
