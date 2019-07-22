using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    [SerializeField] private GameSession gameSession;
    [SerializeField] private Text healthText;
    [SerializeField] private Text pointText;
    [SerializeField] private Text endGamePoint;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = gameSession.live.ToString();
        pointText.text = gameSession.point.ToString();
        endGamePoint.text = gameSession.point.ToString();
    }
}
