using System;
using System.Collections.Generic;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class JSEventDispatcher : JSObject
    {
        private readonly Dictionary<string, List<Action<JSEvent>>> _listeners;

        public JSEventDispatcher()
        {
            this._listeners = new Dictionary<string, List<Action<JSEvent>>>();
        }

        public void addEventListener(string type, Action<JSEvent> listener)
        {
            if (listener == null)
            {
                return;
            }

            if (!this._listeners.ContainsKey(type))
            {
                this._listeners.Add(type, new List<Action<JSEvent>>());
            }

            if (!this._listeners[type].Contains(listener))
            {
                this._listeners[type].Add(listener);
            }
        }

        public void removeEventListener(string type, Action<JSEvent> listener)
        {
            if (listener != null)
            {
                this._listeners[type].Remove(listener);
            }
        }

        public void dispatchEvent(JSEvent evt)
        {
            if (this._listeners.ContainsKey(evt.type))
            {
                var listenerArray = this._listeners[evt.type];
                var actions = listenerArray.ToArray();
                foreach (var t in actions)
                {
                    t(evt);
                }
            }
        }
    }

    // ReSharper restore InconsistentNaming
}
