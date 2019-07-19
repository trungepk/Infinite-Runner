﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    public static GameSession instance;
    public int live = 3;
    [SerializeField] private Obstacle obstacle;

    [SerializeField] GameObject loseImage;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] float slowness = 10f;
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
        if (live >= 1)
        {
            TakeDamage(obstacle.Damage);
            if (live <= 0)
            {
                StartCoroutine(Restart());
            }
        }
        else
        {
        }
    }


    private void TakeDamage(int dmg)
    {
        live -= dmg;
    }

    private IEnumerator Restart()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        loseImage.SetActive(true);
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}