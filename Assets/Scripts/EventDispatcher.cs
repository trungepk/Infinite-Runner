using System;

public class EventDispatcher
{
    /// <summary>
    /// When player falls to lava event.
    /// </summary>
    public static event Action OnFellDown;
    /// <summary>
    /// Invoke OnFellDown event.
    /// </summary>
    public static void RaiseOnFellDown()
    {
        if (OnFellDown != null)
            OnFellDown();
    }

    /// <summary>
    /// When obstacle collides with player event.
    /// </summary>
    public static event Action<Obstacle> OnCollideWithPlayer;
    /// <summary>
    /// Invoke OnCollideWithPlayer event
    /// </summary>
    /// <param name="obstacle"></param>
    public static void RaiseOnCollideWithPlayer(Obstacle obstacle)
    {
        if (OnCollideWithPlayer != null)
            OnCollideWithPlayer(obstacle);
    }

    /// <summary>
    /// When player collides with pickable thing event..
    /// </summary>
    public static event Action<PickableThing> OnPickUp;
    /// <summary>
    /// Invoke OnPickUp event.
    /// </summary>
    /// <param name="pickable"></param>
    public static void RaiseOnPickUp(PickableThing pickable)
    {
        if (OnPickUp != null)
            OnPickUp(pickable);
    }

    /// <summary>
    /// When player's point changed event.
    /// </summary>
    public static event Action OnPointChanged;
    /// <summary>
    /// Invoke OnPointChanged event.
    /// </summary>
    public static void RaiseOnPointChanged()
    {
        if (OnPointChanged != null)
            OnPointChanged();
    }

    /// <summary>
    /// When player's live changed event.
    /// </summary>
    public static event Action OnLiveChanged;
    /// <summary>
    /// Invoke OnLiveChanged event.
    /// </summary>
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
