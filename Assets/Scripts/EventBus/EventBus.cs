using System;
using System.Collections.Generic;
using ServiceLocator;
using UnityEngine.Events;

namespace EventBus
{
    public class EventBus : IService
    {
        private readonly Dictionary<EventList, UnityEvent> _events = new Dictionary<EventList, UnityEvent>();

        public EventBus()
        {
            foreach (EventList eventType in Enum.GetValues(typeof(EventList)))
            {
                _events[eventType] = new UnityEvent();
            }
        }
        
        public void Subscribe(EventList eventId, UnityAction action)
        {
            _events[eventId].AddListener(action);
        }

        public void CallEvent(EventList eventId)
        {
            _events[eventId].Invoke();
        }
    }
}