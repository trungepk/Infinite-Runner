using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    GameSession gameSession;
    [SerializeField] private Animator animator;

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

    private void Start()
    {
        gameSession = GameSession.instance;
        GameSession.OnPointAdded += ChangeUI;
        gameSession.onGetHitCallBack += ChangeUI;

        pointText.text = gameSession.point.ToString();
        healthText.text = gameSession.live.ToString();
        currentMoney.text = "$" + PlayerPrefs.GetInt(Constants.Money, 0);
    }

    private void ChangeUI()
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

    public void SetAnimTrigger(string triggerName)
    {
        foreach(AnimatorControllerParameter p in animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(p.name);
        }
        animator.SetTrigger(triggerName);
    }
}
