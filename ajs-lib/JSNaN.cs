using System;
namespace ajs_lib
{
    public class JSNaN : UncallableUnsettableObject, IJSObject
    {
        public JSType Type { get => JSType.NaN; }
        public IJSObject Prototype { get => null; }
        public string AsCSharpString { get => "NaN"; }
    }
}
