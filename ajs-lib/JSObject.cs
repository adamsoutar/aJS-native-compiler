using System;
using System.Collections.Generic;

namespace ajs_lib
{
    public class JSObject : UncallableObject, IJSObject
    {
        public virtual IJSObject Prototype { get => null; }
        public virtual JSType Type { get => JSType.Object; }
        public virtual string AsCSharpString { get => "TODO"; }
        public virtual Action<string, IJSObject> SetKey { get => PrivSetKey; }
        public virtual Func<string, IJSObject> GetKey { get => PrivGetKey; }

        // Object backing dictionary
        public Dictionary<string, IJSObject> Dict;

        public JSObject()
        {
            Dict = new Dictionary<string, IJSObject>();
        }

        void PrivSetKey (string key, IJSObject value)
        {
            Dict.Add(key, value);
        }

        IJSObject PrivGetKey (string key)
        {
            if (Dict.ContainsKey(key))
            {
                // TODO: Does JS return a reference here or copy?
                return Dict[key];
            } else
            {
                return new JSUndefined();
            }
        }
    }
}
