using System;
namespace ajs_lib.StdLib
{
    // JS global namespace console
    public class Console: JSObject
    {
        public Console()
        {
            SetKey("log", new JSNativeFunction(args =>
            {
                System.Console.WriteLine(args[0].AsCSharpString);
                return new JSUndefined();
            }));
        }
    }
}
