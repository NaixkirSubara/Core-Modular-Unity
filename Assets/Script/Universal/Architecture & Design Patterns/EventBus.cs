using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyStudio.Core.Architecture
{
    public static class EventBus
    {
       
        private static readonly Dictionary<string, Action> Events = new Dictionary<string, Action>();

        
        private static readonly Dictionary<string, object> EventsWithData = new Dictionary<string, object>();

        // ========================================================================
        // BAGIAN 1: Event Kosong (Tanpa Parameter)
        // ========================================================================
        public static void Subscribe(string eventName, Action listener)
        {
            if (Events.TryGetValue(eventName, out Action thisEvent))
            {
                thisEvent += listener;
                Events[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Events.Add(eventName, thisEvent);
            }
        }

        public static void Unsubscribe(string eventName, Action listener)
        {
            if (Events.TryGetValue(eventName, out Action thisEvent))
            {
                thisEvent -= listener;
                Events[eventName] = thisEvent;
            }
        }

        public static void Trigger(string eventName)
        {
            if (Events.TryGetValue(eventName, out Action thisEvent))
            {
                thisEvent?.Invoke();
            }
        }

        // ========================================================================
        // BAGIAN 2: Event dengan DATA (Generics)
        // ========================================================================
        
       
        public static void Subscribe<T>(string eventName, Action<T> listener)
        {
            if (EventsWithData.TryGetValue(eventName, out object existingEvent))
            {
                var callback = (Action<T>)existingEvent;
                callback += listener;
                EventsWithData[eventName] = callback;
            }
            else
            {
                EventsWithData.Add(eventName, listener);
            }
        }

        public static void Unsubscribe<T>(string eventName, Action<T> listener)
        {
            if (EventsWithData.TryGetValue(eventName, out object existingEvent))
            {
                var callback = (Action<T>)existingEvent;
                callback -= listener;
                
                if (callback == null) EventsWithData.Remove(eventName);
                else EventsWithData[eventName] = callback;
            }
        }

        public static void Trigger<T>(string eventName, T data)
        {
            if (EventsWithData.TryGetValue(eventName, out object existingEvent))
            {
                var callback = (Action<T>)existingEvent;
                callback?.Invoke(data);
            }
        }
    }
}