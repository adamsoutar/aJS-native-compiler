using System;
namespace ajs_lib
{
    // This is an object that can't be called or get/set with keys
    // ie. A number
    public abstract class UncallableSilentlyUnsettableObject : UncallableObject
    {
        public virtual Action<string, IJSObject> SetKey { get => PrivSetKey; }
        public virtual Func<string, IJSObject> GetKey { get => PrivGetKey; }

        void PrivSetKey (string key, IJSObject value)
        {
            // This does nothing on this object
        }

        IJSObject PrivGetKey (string key)
        {
            return new JSUndefined();
        }
    }
}
