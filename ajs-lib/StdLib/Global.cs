using System;
using ajs_lib;

namespace ajs_lib.StdLib
{
    // The global object on which all other things are looked up.
    public class Global : JSObject
    {
        public Global()
        {
            SetKey("console", new StdLib.Console());
        }
    }
}
