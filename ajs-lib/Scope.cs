//using System;
//using System.Collections.Generic;

//namespace ajs_lib
//{
//    public class Scope
//    {
//        Dictionary<string, Variable> vars;
//        Scope parent;

//        public Scope(Scope parentScope = null)
//        {
//            vars = new Dictionary<string, Variable>();
//            parent = parentScope;
//        }

//        public Variable LookupVariable (string key)
//        {
//            if (!vars.ContainsKey(key))
//            {
//                if (parent == null)
//                {
//                    // We're probably the global scope
//                    throw new Exception($"ReferenceError: Can't find variable {key}");
//                }

//                return parent.LookupVariable(key);
//            }

//            return vars[key];
//        }

//        public void SetVariable (string key, Variable value)
//        {
//            vars.Add(key, value);
//        }
//    }
//}
