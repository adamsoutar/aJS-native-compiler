using System;
namespace ajs_lib
{
    using NativeFuncSignature = Func<IJSObject[], IJSObject>;

    // This is a C# function wrapped in an IJSObject so it can be added to
    // dictionaries like the global window object etc.
    // TODO: Unsettable object abstract class
    public class JSNativeFunction : JSObject
    {
        public override JSType Type { get => JSType.Function; }
        public override NativeFuncSignature Call { get; }

        public JSNativeFunction(NativeFuncSignature Func)
        {
            Call = Func;
        }
    }
}
