
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

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            // Right, we're set up. What follows is code auto-generated from JS:
scope.SetKey("n", new JSNumber(1));
scope.SetKey("runNextFizzbuzz", new JSNativeFunction(args => {
scope.SetKey("str", new JSString(""));
if (Coercion.CoerceToBool(Operators.StrictlyEqual(Operators.Remainder(scope.GetKey("n"), new JSNumber(3)), new JSNumber(0))).Value) {
scope.SetKey("str", new JSString("Fizz"));

}
if (Coercion.CoerceToBool(Operators.StrictlyEqual(Operators.Remainder(scope.GetKey("n"), new JSNumber(5)), new JSNumber(0))).Value) {
scope.SetKey("str", Operators.Plus(scope.GetKey("str"), new JSString("Buzz")));

}
if (Coercion.CoerceToBool(Operators.StrictlyEqual(scope.GetKey("str"), new JSString(""))).Value) {
scope.SetKey("str", scope.GetKey("n"));

}
((scope.GetKey("console")).GetKey((new JSString("log")).AsCSharpString)).Call(new IJSObject[] {scope.GetKey("str"), });
scope.SetKey("n", Operators.Plus(scope.GetKey("n"), new JSNumber(1)));
if (Coercion.CoerceToBool(Operators.StrictlyNotEqual(scope.GetKey("n"), new JSNumber(101))).Value) {
(scope.GetKey("runNextFizzbuzz")).Call(new IJSObject[] {});

}

return new JSUndefined();}));
(scope.GetKey("runNextFizzbuzz")).Call(new IJSObject[] {});


            watch.Stop();
            System.Console.WriteLine($"Execution Time: { watch.ElapsedMilliseconds}ms");
        }
    }
}
