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
        //if (live >= 1)
        //{
        //    TakeDamage(obstacle.Damage);
        //}
        if (live <= 0)
        {
            StartCoroutine(Restart());
            Destroy(player);
            return;
        }
    }


    //private void TakeDamage(int dmg)
    //{
    //    live -= dmg;
    //}

    private IEnumerator Restart()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        loseImage.SetActive(true);
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        loseImage.SetActive(false);
        retryCanvas.SetActive(true);
    }

    public void AddPoint(int point)
    {
        int progress = this.point % pointTillAddLive; // Current point until add more live
        this.point += point;
        if (progress + point >= pointTillAddLive) { live++; }
    }
}
