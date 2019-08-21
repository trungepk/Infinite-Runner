using System;

public interface IEventHandler
{
    void AddListenner(Action listenner);
    void Invoke();
    void RemoveListenner(Action listenner);
}