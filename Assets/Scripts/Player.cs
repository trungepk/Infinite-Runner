using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private bool isDead;

    private void Update()
    {
        Die();
    }

    private void Die()
    {
        if (GameSession.instance.live <= 0)
        {
            isDead = true;
            //GameSession.instance.ProcessPlayerDead();
        }
    }
}
