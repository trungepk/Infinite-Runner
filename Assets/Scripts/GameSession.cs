using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
    public static GameSession instance;
    public int live = 3;
    [SerializeField] private Obstacle obstacle;
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
        }
        else
        {
            //Restart
        }
    }

    private void TakeDamage(int dmg)
    {
        live -= dmg;
    }
}
