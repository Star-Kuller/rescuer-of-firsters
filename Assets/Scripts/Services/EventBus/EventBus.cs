using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Services
{
    /// <summary>
    /// Шина событий, паттерн, который позволяет инвертировать зависимости
    /// Каждый класс сам решает на какие события ему реагировать
    /// </summary>
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
        
        /// <summary>
        /// Подписаться на событие
        /// </summary>
        /// <param name="eventId">Индефикатор события</param>
        /// <param name="action">Метод, который должен быть вызван вместе с событием</param>
        public void Subscribe(EventList eventId, UnityAction action)
        {
            _events[eventId].AddListener(action);
        }
        
        /// <summary>
        /// Отписаться от события
        /// </summary>
        /// <param name="eventId">Индефикатор события</param>
        /// <param name="action">Метод, который нужно отписать</param>
        public void UnSubscribe(EventList eventId, UnityAction action)
        {
            _events[eventId].RemoveListener(action);
        }

        /// <summary>
        /// Вызвать событие
        /// </summary>
        /// <param name="eventId">Индефикатор события</param>
        public void CallEvent(EventList eventId)
        {
            _events[eventId].Invoke();
        }
    }
}