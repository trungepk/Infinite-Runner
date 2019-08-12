using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    GameSession gameSession;
    //[SerializeField] private Animator animator;

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

    //[Header("Lose game UI")]
    //[SerializeField] float slowness = 10f;
    //[SerializeField] GameObject loseImage;
    //[SerializeField] GameObject playerStatusCanvas;
    //[SerializeField] GameObject retryCanvas;

    private void Start()
    {
        EventDispatcher.OnPointChanged += ChangeStatusUI;
        EventDispatcher.OnLiveChanged += ChangeStatusUI;
        //EventDispatcher.OnLoseGame += DisplayLoseGameUI;
        gameSession = GameSession.instance;

        pointText.text = gameSession.point.ToString();
        healthText.text = gameSession.live.ToString();
        currentMoney.text = "$" + PlayerPrefs.GetInt(Constants.Money, 0);
    }

    private void OnDisable()
    {
        EventDispatcher.OnPointChanged -= ChangeStatusUI;
        EventDispatcher.OnLiveChanged -= ChangeStatusUI;
        //EventDispatcher.OnLoseGame -= DisplayLoseGameUI;
    }

    private void ChangeStatusUI()
    {
        pointText.text = gameSession.point.ToString();
        endGamePoint.text = gameSession.point.ToString();
        bestScore.text = PlayerPrefs.GetInt(Constants.BestScore, 0).ToString();
        healthText.text = gameSession.live.ToString();
    }

    public void DisplayItemInfo(string itemName, string itemDescription, int cost)
    {
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        itemCost.text = "$" + cost;
    }

    public void DisplayMoneyAfterPurchase()
    {
        currentMoney.text = "$" + PlayerPrefs.GetInt(Constants.Money);
    } 

    //private IEnumerator DisplayLoseGameUI()
    //{
    //    Time.timeScale = 1f / slowness;
    //    Time.fixedDeltaTime /= slowness;
    //    loseImage.SetActive(true);
    //    yield return new WaitForSeconds(1f);
    //    Time.timeScale = 1f;
    //    Time.fixedDeltaTime *= slowness;
    //    loseImage.SetActive(false);
    //    playerStatusCanvas.SetActive(false);
    //    retryCanvas.SetActive(true);
    //}

    //public void SetAnimTrigger(string triggerName)
    //{
    //    foreach(AnimatorControllerParameter p in animator.parameters)
    //    {
    //        if (p.type == AnimatorControllerParameterType.Trigger)
    //            animator.ResetTrigger(p.name);
    //    }
    //    animator.SetTrigger(triggerName);
    //}
}
