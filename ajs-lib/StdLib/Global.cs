using System;
using ajs_lib;

namespace ajs_lib.StdLib
{
    // The global object on which all other things are looked up.
    public class Global : JSObject
    {
        // Extends of the super constructor while providing default args
        // (it throws when lookup fails)
        public Global() : base(null, true)
        {
            SetKey("console", new StdLib.Console());
        }
    }
}
