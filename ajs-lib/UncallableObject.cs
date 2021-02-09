using System;
namespace ajs_lib
{
    public class UncallableObject
    {
        public virtual Func<IJSObject[], IJSObject> Call { get => PrivCall; }

        IJSObject PrivCall(IJSObject[] args)
        {
            // TODO: A proper TypeError class
            throw new Exception("TypeError: Not a function");
        }
    }
}
