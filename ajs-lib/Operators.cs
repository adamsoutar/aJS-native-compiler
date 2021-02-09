using System;
namespace ajs_lib
{
    public static class Operators
    {
        public static IJSObject Plus (IJSObject a, IJSObject b)
        {
            if (a.Type == JSType.Number && b.Type == JSType.Number)
            {
                var aN = (JSNumber)a;
                var bN = (JSNumber)b;
                return new JSNumber(aN.Value + bN.Value);
            }
            else if (a.Type == JSType.String || b.Type == JSType.String)
            {
                return new JSString($"{a.AsCSharpString}{b.AsCSharpString}");
            }
            else
            {
                throw new NotImplementedException("Addition of those two types is not supported");
            }
        }
    }
}
