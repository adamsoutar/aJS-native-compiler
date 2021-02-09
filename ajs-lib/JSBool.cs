using System;
namespace ajs_lib
{
    public class JSBool : UncallableSilentlyUnsettableObject, IJSObject
    {
        public IJSObject Prototype { get => null; }
        public JSType Type { get => JSType.Boolean; }
        // Can't rely on C#'s ToString(), it capitalises True and False
        public string AsCSharpString { get => Value ? "true" : "false"; }

        public bool Value;

        public JSBool(bool b)
        {
            Value = b;
        }
    }
}
