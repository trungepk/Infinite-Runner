using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    public static GameSession instance;
    public int live = 3;
    public int point;
    [SerializeField] private int pointTillAddLive = 50;
    [SerializeField] private Obstacle obstacle;

    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject retryCanvas;
    [SerializeField] GameObject playerStatusCanvas;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] float slowness = 10f;

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject player;
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
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(Constants.BestScore);
    }
}
