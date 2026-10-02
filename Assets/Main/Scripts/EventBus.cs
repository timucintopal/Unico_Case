using System;

namespace Main.Scripts
{
    public interface IEvent { }
    public readonly struct GameStartRequestedEvent : IEvent { }
    public static class EventBus<T> where T : struct, IEvent
    {
        private static Action<T> handlers;
 
        public static void Subscribe(Action<T> handler) => handlers += handler;
 
        public static void Unsubscribe(Action<T> handler) => handlers -= handler;
 
        public static void Publish(T evt) => handlers?.Invoke(evt);
    }

}
