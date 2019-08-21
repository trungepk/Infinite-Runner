using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    GameSession gameSession;

    [Header("Player status UI")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text pointText;

    [Header("Game over UI")]
    [SerializeField] private Text endGamePoint;
    [SerializeField] private Text bestScore;

    [Header("Shop UI")]
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Text itemCost;
    [SerializeField] private Text currentMoney;

    [Header("Lose game UI")]
    [SerializeField] float slowness = 10f;
    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject playerStatusCanvas;
    [SerializeField] GameObject retryCanvas;

    private void Start()
    {
        EventDispatcher.Subscribe(EventID.OnPointChanged, ChangeStatusUI);
        EventDispatcher.Subscribe(EventID.OnLiveChanged, ChangeStatusUI);
        EventDispatcher.Subscribe(EventID.OnMoneyChanged, DisplayMoneyAfterTrading);
        EventDispatcher.Subscribe(EventID.OnSelectItem, DisplayItemInfo);
        EventDispatcher.Subscribe(EventID.OnLoseGame, DisplayLoseGameUI);

        gameSession = GameSession.instance;
        pointText.text = gameSession.point.ToString();
        healthText.text = gameSession.live.ToString();
        currentMoney.text = "$" + PlayerPrefs.GetInt(Constants.Money, 0);
    }

    private void OnDisable()
    {
        EventDispatcher.Unsubscribe(EventID.OnPointChanged, ChangeStatusUI);
        EventDispatcher.Unsubscribe(EventID.OnLiveChanged, ChangeStatusUI);
        EventDispatcher.Unsubscribe(EventID.OnMoneyChanged, DisplayMoneyAfterTrading);
        EventDispatcher.Unsubscribe(EventID.OnSelectItem, DisplayItemInfo);
        EventDispatcher.Unsubscribe(EventID.OnLoseGame, DisplayLoseGameUI);
    }

    private void ChangeStatusUI()
    {
        pointText.text = gameSession.point.ToString();
        endGamePoint.text = gameSession.point.ToString();
        bestScore.text = PlayerPrefs.GetInt(Constants.BestScore, 0).ToString();
        healthText.text = gameSession.live.ToString();
    }

    private void DisplayItemInfo(object obj)
    {
        ShopItem item = obj as ShopItem;
        this.itemName.text = item.itemName;
        this.itemDescription.text = item.itemDescription;
        itemCost.text = "$" + item.cost;
    }

    private void DisplayMoneyAfterTrading()
    {
        currentMoney.text = "$" + PlayerPrefs.GetInt(Constants.Money);
    }

    private void DisplayLoseGameUI()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        loseImage.SetActive(true);
        StartCoroutine(DisplayRetryUI());
    }

    private IEnumerator DisplayRetryUI()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        loseImage.SetActive(false);
        playerStatusCanvas.SetActive(false);
        retryCanvas.SetActive(true);
    }
}
