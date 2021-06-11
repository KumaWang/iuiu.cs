using System;
using System.Collections.Generic;
using System.Text;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class JSArray : JSObject
    {
        private readonly List<object> _elements;

        public JSArray(int size)
        {
            this._elements = new List<object>();
            this.ensureIndex(size - 1);
        }

        public JSArray(params object[] values)
        {
            this._elements = new List<object>(values);
        }

        public dynamic this[int index]
        {
            get { return index < 0 || index >= this.length ? null : this._elements[index]; }
            set
            {
                this.ensureIndex(index);
                this._elements[index] = value;
            }
        }

        public int length
        {
            get { return this._elements.Count; }
            set { this.resize(value); }
        }

        public dynamic pop()
        {
            throw new NotImplementedException();
        }

        public int push(params dynamic[] items)
        {
            if (items != null && items.Length > 0)
            {
                this._elements.AddRange(items);
            }
            return this.length;
        }

        public JSArray reverse()
        {
            this._elements.Reverse();
            return this;
        }

        public dynamic shift()
        {
            throw new NotImplementedException();
        }

        public void sort(Comparison<object> sortfunction = null)
        {
            if (sortfunction != null)
            {
                this._elements.Sort(sortfunction);
            }
            else
            {
                this._elements.Sort();
            }
        }

        public void splice(int index, int howmany, params dynamic[] items)
        {
            this._elements.RemoveRange(index, howmany);
            this._elements.InsertRange(index, items);
        }

        public int unshift(dynamic element1, params dynamic[] elementN)
        {
            throw new NotImplementedException();
        }

        public JSArray concat(params JSArray[] arrays)
        {
            var result = new JSArray();
            result._elements.AddRange(this._elements);
            foreach (var array in arrays)
            {
                result._elements.AddRange(array._elements);
            }
            return result;
        }

        public string join(string sep = ",")
        {
            return string.Join(sep, this._elements);
        }

        public JSArray slice(int begin = 0)
        {
            return this.slice(begin, this._elements.Count);
        }

        public JSArray slice(int begin, int end)
        {
            while (begin < 0)
            {
                begin += this._elements.Count;
            }

            while (end < 0)
            {
                end += this._elements.Count;
            }

            var result = new JSArray();
            if (end > begin)
            {
                for (var i = begin; i < end; i++)
                {
                    result.push(this._elements[i]);
                }
            }

            return result;
        }

        public String toString()
        {
            var first = true;
            var sb = new StringBuilder();
            sb.Append("[");
            for (var i = 0; i < this._elements.Count; i++)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                first = false;

                var val = this._elements[i];
                if (val != null)
                {
                    sb.Append(val);
                }
                else
                {
                    var undefined = 1;
                    for (var j = i + 1; j < this._elements.Count; j++)
                    {
                        if (j >= this._elements.Count || this._elements[j] != null)
                        {
                            break;
                        }
                        undefined++;
                    }
                    i += undefined - 1;
                    sb.AppendFormat("null x {0}", undefined);
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        public int indexOf(object searchElement, int fromIndex = 0)
        {
            return this._elements.IndexOf(searchElement, fromIndex);
        }

        public int lastIndexOf(object searchElement, int fromIndex = int.MaxValue)
        {
            return this._elements.LastIndexOf(searchElement, Math.Min(this._elements.Count, fromIndex));
        }

        private void resize(int size)
        {
            if (size < 0)
            {
                size = 0;
            }
            if (size < this._elements.Count)
            {
                throw new NotImplementedException();
            }
            else if (size > this._elements.Count)
            {
                this.ensureIndex(size - 1);
            }
        }

        private void ensureIndex(int index)
        {
            var amount = index - this._elements.Count;
            while (amount-- >= 0)
            {
                this._elements.Add(null);
            }
        }

        public class Prototype
        {
            public ApplyWrapper push
            {
                get
                {
                    return new ApplyWrapper((Action<JSArray, JSArray>)((thisArg, argsArray) =>
                    {
                        for (var i = 0; i < argsArray.length; i++)
                        {
                            thisArg.push(argsArray[i]);
                        }
                    }));
                }
            }
        }

        public static readonly Prototype prototype = new Prototype();
    }

    // ReSharper restore InconsistentNaming
}
