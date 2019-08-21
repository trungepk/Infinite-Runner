using System;

public interface IEventHandlerOneArg
{
    void AddListenner(Action<object> listenner);
    void Invoke(object obj);
    void RemoveListenner(Action<object> listenner);
}
