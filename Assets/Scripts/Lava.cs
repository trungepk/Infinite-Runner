﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.PlayerTag)
        {
            Destroy(other);
            GameSession.instance.live = 0;
            GameSession.instance.ProcessPlayerDead();
        }
    }
}
