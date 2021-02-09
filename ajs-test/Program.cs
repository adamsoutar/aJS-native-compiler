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
        }
    }
}