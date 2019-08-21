using System;

public class EventHandler : IEventHandler, IEventHandlerOneArg
{
    private event Action MyEvent = null;

    public void AddListenner(Action listenner)
    {
        MyEvent += listenner;
    }

    public void RemoveListenner(Action listenner)
    {
        MyEvent -= listenner;
    }

    public void Invoke()
    {
        MyEvent();
    }

    private event Action<object> MyEventOneArg = null;
    public void AddListenner(Action<object> listenner)
    {
        MyEventOneArg += listenner;
    }

    public void RemoveListenner(Action<object> listenner)
    {
        MyEventOneArg -= listenner;
    }

    public void Invoke(object obj)
    {
        MyEventOneArg(obj);
    }
}

	
