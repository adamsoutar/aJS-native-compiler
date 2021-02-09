using System;
namespace ajs_lib
{
    // This is an object that can't be called or get/set with keys
    // (JS calls this 'Not an object' but we call everything an object)
    // ie. undefined/null
    public abstract class UncallableUnsettableObject : UncallableObject
    {
        public virtual Action<string, IJSObject> SetKey { get => PrivSetKey; }
        public virtual Func<string, IJSObject> GetKey { get => PrivGetKey; }

        void PrivSetKey (string key, IJSObject value)
        {
            throw new Exception("TypeError: Not an object (set)");
        }

        IJSObject PrivGetKey (string key)
        {
            throw new Exception("TypeError: Not an object (get)");
        }
    }
}
