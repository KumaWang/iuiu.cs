using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class JSObject : DynamicObject, IEnumerable
    {
        private readonly Dictionary<object, object> _values = new Dictionary<object, object>();

        public dynamic this[object key]
        {
            get { return this._values.ContainsKey(key) ? this._values[key] : null; }
            set { this._values[key] = value; }
        }

        public bool delete(object key)
        {
            return this._values.Remove(key);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            object data;
            result = this._values.TryGetValue(binder.Name, out data) ? data : null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this._values[binder.Name] = value;
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            return this._values.Keys.GetEnumerator();
        }

        public static bool eval(dynamic o)
        {
            if (o != null)
            {
                if (o is bool)
                {
                    return o;
                }
                if (o is int)
                {
                    return o != 0;
                }
                return true;
            }
            return false;
        }

        public static T safe<T>(dynamic o)
        {
            return o ?? default(T);
        }

        public static dynamic create(dynamic something)
        {
            return something != null ? unpack(something, new JSObject()) : null;
        }

        private static JSObject unpack(dynamic something, JSObject ric)
        {
            var dic = ((object)something).GetType().GetProperties().ToDictionary<PropertyInfo, string, object>(p => p.Name, p => p.GetValue(something, null));
            foreach (var key in dic.Keys)
            {
                var o = dic[key];
                if (o != null && isAnonymousType(o.GetType()))
                {
                    o = unpack(o, new JSObject());
                }
                ric[key] = o;
            }
            return ric;
        }

        private static bool isAnonymousType(Type type)
        {
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) ||
                       type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }

    // ReSharper restore InconsistentNaming
}
