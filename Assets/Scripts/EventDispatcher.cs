using System;

public class EventDispatcher
{
    /// <summary>
    /// Player falls to lava event.
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
    /// Obstacle collides with player event.
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
    /// Player collides with pickable thing event..
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
    /// Player's point changed event.
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
    /// Player's live changed event.
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

    /// <summary>
    /// Money changed after trading event.
    /// </summary>
    public static event Action OnMoneyChanged;
    /// <summary>
    /// Invoke OnMoneyChanged event.
    /// </summary>
    public static void RaiseOnMoneyChanged()
    {
        if (OnMoneyChanged != null)
            OnMoneyChanged();
    }

    /// <summary>
    /// Select item in shop event.
    /// </summary>
    public static event Action<string, string, int> OnSelectItem;
    /// <summary>
    /// Invoke OnSelectItem event.
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="itemDescription"></param>
    /// <param name="cost"></param>
    public static void RaiseOnSelectItem(string itemName, string itemDescription, int cost)
    {
        if (OnSelectItem != null)
            OnSelectItem(itemName, itemDescription, cost);
    }

    public static event Action OnLoseGame;
    public static void RaiseOnLoseGame()
    {
        if (OnLoseGame != null)
            OnLoseGame();
    }

    public static event Action OnBuyItem;
    public static void RaiseOnBuyItem()
    {
        if (OnBuyItem != null)
            OnBuyItem();
    }
}
