
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
            var consoleLog = ((jsGlobal.GetKey("console")).GetKey((jsGlobal.GetKey("log")).AsCSharpString)); //.Call(new IJSObject[] { new JSString("Hello") });
            System.Console.WriteLine(consoleLog.Type);
        }
    }
}
