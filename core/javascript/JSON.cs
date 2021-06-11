using System;
using System.Collections.Generic;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    static class JSON
    {
        public static dynamic parse(string text)
        {
            var javaScriptSerializer = new JavaScriptSerializer {MaxJsonLength = text.Length};
            var deserialize = javaScriptSerializer.Deserialize<IDictionary<string, object>>(text);
            return parseJSONTree(deserialize, new JSObject());
        }

        public static string stringify(dynamic value)
        {
            throw new NotImplementedException();
        }

        private static dynamic parseJSONTree(object something, dynamic ric)
        {
            if (something is IDictionary<string, object>)
            {
                var result = something as IDictionary<string, object>;
                foreach (var key in result.Keys)
                {
                    var o = result[key];
                    if (o != null)
                    {
                        o = parseJSONTree(o, o is Array ? new JSArray() : new JSObject());
                    }
                    ric[key] = o;
                }
                return ric;
            }
            if (something is Array)
            {
                var result = something as Array;
                for (var i = 0; i < result.Length; i++)
                {
                    var o = result.GetValue(i);
                    ric[i] = parseJSONTree(o, o is Array ? new JSArray() : new JSObject());
                }
                return ric;
            }
            return something;
        }
    }

    // ReSharper restore InconsistentNaming
}
