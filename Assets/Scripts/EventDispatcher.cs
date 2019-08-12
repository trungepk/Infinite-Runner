using System;

public class EventDispatcher
{
    public static event Action OnFellDown;
    public static void RaiseOnFellDown()
    {
        if (OnFellDown != null)
            OnFellDown();
    }

    public static event Action<Obstacle> OnCollideWithPlayer;
    public static void RaiseOnCollideWithPlayer(Obstacle obstacle)
    {
        if (OnCollideWithPlayer != null)
            OnCollideWithPlayer(obstacle);
    }

    public static event Action<PickableThing> OnPickUp;
    public static void RaiseOnPickUp(PickableThing pickable)
    {
        if (OnPickUp != null)
            OnPickUp(pickable);
    }

    public static event Action OnPointChanged;
    public static void RaiseOnPointChanged()
    {
        if (OnPointChanged != null)
            OnPointChanged();
    }

    public static event Action OnLiveChanged;
    public static void RaiseOnLiveChanged()
    {
        if (OnLiveChanged != null)
            OnLiveChanged();
    }

    //public delegate IEnumerator onLoseGame();
    //public static event onLoseGame OnLoseGame;
    //public static void RaiseOnLoseGame()
    //{
    //    if (OnLoseGame != null)
    //        StartCoroutine(OnLoseGame());
    //}
}
