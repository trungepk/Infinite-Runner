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
        EventDispatcher.OnPickUp += AddPoint;
        EventDispatcher.OnPickUp += AddMoney;
        EventDispatcher.OnCollideWithPlayer += ReduceLive;
        EventDispatcher.OnCollideWithPlayer += ProcessPlayerDead;
        EventDispatcher.OnFellDown += Shredded;
    }

    private void OnDisable()
    {
        EventDispatcher.OnPickUp -= AddPoint;
        EventDispatcher.OnPickUp -= AddMoney;
        EventDispatcher.OnCollideWithPlayer -= ReduceLive;
        EventDispatcher.OnCollideWithPlayer -= ProcessPlayerDead;
        EventDispatcher.OnFellDown -= Shredded;
    }

    private void ProcessPlayerDead(Obstacle obstacle)
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
        EventDispatcher.RaiseOnLiveChanged();
    }

    private void Lose()
    {
        EventDispatcher.RaiseOnLoseGame();
        Destroy(player);
        if (point > PlayerPrefs.GetInt(Constants.BestScore, 0))
        {
            PlayerPrefs.SetInt(Constants.BestScore, point);
        }
    }

    private void AddPoint(PickableThing pickable)
    {
        int progress = point % pointTillAddLive; // Current point until add more live
        point += pickable.Point;
        if (progress + pickable.Point >= pointTillAddLive) { live++; }

        EventDispatcher.RaiseOnPointChanged();
    }

    private void AddMoney(PickableThing pickable)
    {
        int currentMoney = PlayerPrefs.GetInt(Constants.Money);
        PlayerPrefs.SetInt(Constants.Money, currentMoney + pickable.MoneyValue);
    }

    private void ReduceLive(Obstacle obstacle)
    {
        live -= obstacle.Damage;
        EventDispatcher.RaiseOnLiveChanged();
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
