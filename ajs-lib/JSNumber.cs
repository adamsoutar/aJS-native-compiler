using System;
namespace ajs_lib
{
    public class JSNumber : UncallableSilentlyUnsettableObject, IJSObject
    {
        public IJSObject Prototype { get => null; }
        public JSType Type { get => JSType.Number; }
        public string AsCSharpString { get => Value.ToString(); }

        public double Value;

        public JSNumber(double n)
        {
            Value = n;
        }
    }
}
