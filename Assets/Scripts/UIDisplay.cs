using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    [SerializeField] private GameSession gameSession;

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
        currentMoney.text = PlayerPrefs.GetInt(Constants.Money, 0).ToString();
    }

    void Update () {
        healthText.text = gameSession.live.ToString();
        pointText.text = gameSession.point.ToString();
        endGamePoint.text = gameSession.point.ToString();
        bestScore.text = PlayerPrefs.GetInt(Constants.BestScore, 0).ToString();
    }

    public void DisplayItemInfo(string itemName, string itemDescription, int cost)
    {
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        itemCost.text = cost.ToString();
    }

    public void DisplayMoneyAfterPurchase()
    {
        currentMoney.text = PlayerPrefs.GetInt(Constants.Money, 0).ToString();
    } 
}
