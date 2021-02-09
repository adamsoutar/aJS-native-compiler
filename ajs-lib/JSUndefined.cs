using System;
namespace ajs_lib
{
    public class JSUndefined : UncallableUnsettableObject, IJSObject
    {
        public JSType Type { get => JSType.Undefined; }
        public IJSObject Prototype { get => null; }
        public string AsCSharpString { get => "undefined"; }
    }
}
