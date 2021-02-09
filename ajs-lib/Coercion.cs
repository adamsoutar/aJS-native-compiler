using System;
namespace ajs_lib
{
    // Run-time JS-like type coercion
    public static class Coercion
    {
        public static JSBool CoerceToBool (IJSObject a)
        {
            bool outcome;
            switch (a.Type)
            {
                case JSType.String:
                    outcome = ((JSString)a).Value == "";
                    break;
                case JSType.Boolean:
                    outcome = ((JSBool)a).Value;
                    break;
                case JSType.Number:
                    outcome = ((JSNumber)a).Value == 0;
                    break;
                case JSType.Null:
                    outcome = false;
                    break;
                case JSType.Undefined:
                    outcome = false;
                    break;
                default:
                    outcome = true;
                    break;
            }
            return new JSBool(outcome);
        }
    }
}
