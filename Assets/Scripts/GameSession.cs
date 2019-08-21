using UnityEngine;

public class GameSession : MonoBehaviour {
    #region Singleton
    public static GameSession instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Player status")]
    public int live = 2;
    [HideInInspector] public int point;
    [SerializeField] private GameObject player;
    [SerializeField] private int pointTillAddLive = 50;

    private void Start()
    {
        EventDispatcher.Subscribe(EventID.OnPickedUp, AddPoint);
        EventDispatcher.Subscribe(EventID.OnPickedUp, AddMoney);
        EventDispatcher.Subscribe(EventID.OnCollideWithPlayer, ReduceLive);
        EventDispatcher.Subscribe(EventID.OnCollideWithPlayer, ProcessPlayerDead);
        EventDispatcher.Subscribe(EventID.OnFellDown, Shredded);
    }

    private void OnDisable()
    {
        EventDispatcher.Unsubscribe(EventID.OnPickedUp, AddPoint);
        EventDispatcher.Unsubscribe(EventID.OnPickedUp, AddMoney);
        EventDispatcher.Unsubscribe(EventID.OnCollideWithPlayer, ReduceLive);
        EventDispatcher.Unsubscribe(EventID.OnCollideWithPlayer, ProcessPlayerDead);
        EventDispatcher.Unsubscribe(EventID.OnFellDown, Shredded);
    }

    private void ProcessPlayerDead(object obj)
    {
        if (live <= 0)
        {
            Lose();
        }
    }

    private void Shredded()
    {
        live = 0;
        Lose();
        EventDispatcher.RaiseEvent(EventID.OnLiveChanged);
    }

    private void Lose()
    {
        EventDispatcher.RaiseEvent(EventID.OnLoseGame);
        Destroy(player);
        if (point > PlayerPrefs.GetInt(Constants.BestScore, 0))
        {
            PlayerPrefs.SetInt(Constants.BestScore, point);
        }
    }

    private void AddPoint(object obj)
    {
        PickableThing pickable = obj as PickableThing;
        int progress = point % pointTillAddLive; // Current point until add more live
        point += pickable.Point;
        if (progress + pickable.Point >= pointTillAddLive) { live++; }

        EventDispatcher.RaiseEvent(EventID.OnPointChanged);
    }

    private void AddMoney(object obj)
    {
        PickableThing pickable = obj as PickableThing;
        int currentMoney = PlayerPrefs.GetInt(Constants.Money);
        PlayerPrefs.SetInt(Constants.Money, currentMoney + pickable.MoneyValue);
    }

    private void ReduceLive(object obj)
    {
        Obstacle obstacle = obj as Obstacle;
        live -= obstacle.Damage;
        EventDispatcher.RaiseEvent(EventID.OnLiveChanged);
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(Constants.BestScore);
    }

    public void ResetMoney(int initialMoney)
    {
        PlayerPrefs.SetInt(Constants.Money, initialMoney);
    }
}
