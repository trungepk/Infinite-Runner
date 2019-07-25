using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Obstacle obstacle;
    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject retryCanvas;
    [SerializeField] GameObject playerStatusCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] AudioClip loseSFX;

    [SerializeField] float slowness = 10f;

    public delegate void OnInteractWithObject();
    public OnInteractWithObject onAddPointCallBack;
    public OnInteractWithObject onGetHitCallBack;

    public void ProcessPlayerDead()
    {
        if (live <= 0)
        {
            StartCoroutine(Lose());
            Destroy(player);
            if(point > PlayerPrefs.GetInt(Constants.BestScore, 0))
            {
                PlayerPrefs.SetInt(Constants.BestScore, point);
            }

            return;
        }
    }

    private IEnumerator Lose()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        loseImage.SetActive(true);
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        loseImage.SetActive(false);
        playerStatusCanvas.SetActive(false);
        retryCanvas.SetActive(true);
    }

    public void AddPoint(int point)
    {
        int progress = this.point % pointTillAddLive; // Current point until add more live
        this.point += point;
        if (progress + point >= pointTillAddLive) { live++; }

        if (onAddPointCallBack != null)
            onAddPointCallBack.Invoke();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(Constants.BestScore);
    }

    public void AddMoney(int money)
    {
        int currentMoney = PlayerPrefs.GetInt(Constants.Money);
        PlayerPrefs.SetInt(Constants.Money, currentMoney + money);
    }

    public void ResetMoney(int initialMoney)
    {
        PlayerPrefs.SetInt(Constants.Money, initialMoney);
    }
}
