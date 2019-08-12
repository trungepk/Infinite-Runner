using System.Collections;
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
    public int point;
    public int money;
    [SerializeField] private int pointTillAddLive = 50;

    [Header("References")]
    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject retryCanvas;
    [SerializeField] GameObject playerStatusCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] AudioClip loseSFX;

    [SerializeField] float slowness = 10f;

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
            //StartCoroutine(Lose());
            Lose();
            Destroy(player);
            if(point > PlayerPrefs.GetInt(Constants.BestScore, 0))
            {
                PlayerPrefs.SetInt(Constants.BestScore, point);
            }

            return;
        }
    }

    private IEnumerator Lose()
    //private void Lose()
    {
        //EventDispatcher.RaiseOnLoseGame();
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        loseImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        loseImage.SetActive(false);
        playerStatusCanvas.SetActive(false);
        retryCanvas.SetActive(true);
    }

    private void AddPoint(PickableThing pickable)
    {
        int progress = point % pointTillAddLive; // Current point until add more live
        point += pickable.Point;
        if (progress + pickable.Point >= pointTillAddLive) { live++; }

        EventDispatcher.RaiseOnPointChanged();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(Constants.BestScore);
    }

    private void AddMoney(PickableThing pickable)
    {
        int currentMoney = PlayerPrefs.GetInt(Constants.Money);
        PlayerPrefs.SetInt(Constants.Money, currentMoney + pickable.MoneyValue);
    }

    public void ResetMoney(int initialMoney)
    {
        PlayerPrefs.SetInt(Constants.Money, initialMoney);
    }

    private void ReduceLive(Obstacle obstacle)
    {
        live -= obstacle.Damage;
        EventDispatcher.RaiseOnLiveChanged();
    }

    private void Shredded()
    {
        live = 0;
        //StartCoroutine(Lose());
        Lose();
        Destroy(player);
        if (point > PlayerPrefs.GetInt(Constants.BestScore, 0))
        {
            PlayerPrefs.SetInt(Constants.BestScore, point);
        }
        EventDispatcher.RaiseOnLiveChanged();
    }
}
