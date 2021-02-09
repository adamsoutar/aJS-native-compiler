using System;
namespace ajs_lib
{
    // TODO: This is NOT actually a silently unsettable object,
    //       indexes return/set chars
    public class JSString: UncallableSilentlyUnsettableObject, IJSObject
    {
        public IJSObject Prototype { get => null; }
        public JSType Type { get => JSType.String; }
        public string AsCSharpString { get => Value; }

        public string Value;

        public JSString(string str)
        {
            Value = str;
        }

        public string Stringify ()
        {
            return Value;
        }
    }
}
