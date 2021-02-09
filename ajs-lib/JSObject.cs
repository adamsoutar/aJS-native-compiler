using System;
using System.Collections.Generic;

namespace ajs_lib
{
    public class JSObject : UncallableObject, IJSObject
    {
        public virtual IJSObject Prototype { get; }
        public virtual JSType Type { get => JSType.Object; }
        public virtual string AsCSharpString { get => "TODO"; }
        public virtual Action<string, IJSObject> SetKey { get => PrivSetKey; }
        public virtual Func<string, IJSObject> GetKey { get => PrivGetKey; }

        // Object backing dictionary
        public Dictionary<string, IJSObject> Dict;
        bool ThrowOnLookupFail;

        public JSObject(IJSObject prototype = null, bool throwOnLookupFail = false)
        {
            Dict = new Dictionary<string, IJSObject>();
            Prototype = prototype;
            // If we're, say, the global object, we wanna throw on key lookup failure.
            ThrowOnLookupFail = throwOnLookupFail;
        }

        void PrivSetKey (string key, IJSObject value)
        {
            if (Dict.ContainsKey(key)) Dict[key] = value;
            else Dict.Add(key, value);
        }

        IJSObject PrivGetKey (string key)
        {
            if (Dict.ContainsKey(key))
            {
                // TODO: Does JS return a reference here or copy?
                return Dict[key];
            } else
            {
                if (Prototype == null)
                {
                    if (ThrowOnLookupFail)
                    {
                        throw new Exception($"ReferenceError: Can't find variable {key}");
                    }
                    return new JSUndefined();
                }
                return Prototype.GetKey(key);
            }
        }
    }
}
