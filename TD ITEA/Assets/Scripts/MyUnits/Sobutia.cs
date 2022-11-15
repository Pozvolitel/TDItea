namespace CustomEvents
{
    public static class EventAggregator
    {
        public static void Subscrible<T>(System.Action<object, T> eventHandler)
        {
            Event<T>.EventHolder += eventHandler;
        }

        public static void UnSubscrible<T>(System.Action<object, T> eventHandler)
        {
            Event<T>.EventHolder -= eventHandler;
        }

        public static void Post<T>(object sender, T eventData)
        {
            Event<T>.Post(sender, eventData);
        }

        private static class Event<T>
        {
            public static event System.Action<object, T> EventHolder;

            public static void Post(object sender, T eventData)
            {
                EventHolder.Invoke(sender, eventData);
            }
        }
    }
}
