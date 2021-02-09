using System;

namespace ajs_lib
{
    public interface IJSObject
    {
        IJSObject Prototype { get; }
        JSType Type { get; }

        Func<IJSObject[], IJSObject> Call { get; }

        Func<string, IJSObject> GetKey { get; }
        Action<string, IJSObject> SetKey { get; }

        string AsCSharpString { get; }
    }
}
