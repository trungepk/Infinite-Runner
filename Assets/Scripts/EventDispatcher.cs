using System;
using System.Collections.Generic;

public class EventDispatcher
{
    private static Dictionary<EventID, IEventHandler> eventDictionary = new Dictionary<EventID, IEventHandler>();

    public static void RaiseEvent(EventID eventID)
    {
        IEventHandler thisEvent = null;
        if (eventDictionary.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void Subscribe(EventID eventID, Action listener)
    {
        IEventHandler thisEvent = null;
        if(eventDictionary.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.AddListenner(listener);
        }
        else
        {
            thisEvent = new EventHandler();
            thisEvent.AddListenner(listener);
            eventDictionary.Add(eventID, thisEvent);
        }
    }

    public static void Unsubscribe(EventID eventID, Action listener)
    {
        IEventHandler thisEvent = null;
        if (eventDictionary.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.RemoveListenner(listener);
        }
    }

    private static Dictionary<EventID, IEventHandlerOneArg> eventDictionaryOneArg = new Dictionary<EventID, IEventHandlerOneArg>();

    public static void RaiseEvent(EventID eventID, object obj)
    {
        IEventHandlerOneArg thisEvent = null;
        if (eventDictionaryOneArg.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.Invoke(obj);
        }
    }

    public static void Subscribe(EventID eventID, Action<object> listener)
    {
        IEventHandlerOneArg thisEvent = null;
        if (eventDictionaryOneArg.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.AddListenner(listener);
        }
        else
        {
            thisEvent = new EventHandler();
            thisEvent.AddListenner(listener);
            eventDictionaryOneArg.Add(eventID, thisEvent);
        }
    }

    public static void Unsubscribe(EventID eventID, Action<object> listener)
    {
        IEventHandlerOneArg thisEvent = null;
        if (eventDictionaryOneArg.TryGetValue(eventID, out thisEvent))
        {
            thisEvent.RemoveListenner(listener);
        }
    }
}
