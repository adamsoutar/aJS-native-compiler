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
                throw new NotImplementedException("Addition of those two types is not implemented");
            }
        }

        // TODO: Alg. isn't absolutely perfect
        public static JSBool StrictlyEqual(IJSObject a, IJSObject b)
        {
            if (a.Type != b.Type)
            {
                return new JSBool(false);
            }
            return Equal(a, b);
        }

        public static JSBool StrictlyNotEqual (IJSObject a, IJSObject b)
        {
            return new JSBool(
                !StrictlyEqual(a, b).Value
            );
        }

        // TODO: Implement this properly with type conversion
        //       For now we'll only support getting here through StrictEquality
        public static JSBool Equal (IJSObject a, IJSObject b)
        {
            bool areEqual;
            switch (a.Type)
            {
                case JSType.Number:
                    areEqual = ((JSNumber)a).Value == ((JSNumber)b).Value;
                    break;
                default:
                    throw new NotImplementedException($"Equality between type {a.Type} and {b.Type} is not implemented");
            }

            return new JSBool(areEqual);
        }

        public static JSBool NotEqual (IJSObject a, IJSObject b)
        {
            return new JSBool(
                !Equal(a, b).Value
            );
        }
    }
}
