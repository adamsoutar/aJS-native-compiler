using System;
namespace ajs_lib
{
    public class JSVariable
    {
        public IJSObject Value;
        public bool IsConst;

        public JSVariable(IJSObject Value, bool IsConst)
        {
            this.Value = Value;
            this.IsConst = IsConst;
        }
    }
}
