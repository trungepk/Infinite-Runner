using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private int damage = 1;

    private void Start()
    {
    }

    public int Damage { get { return damage; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameSession.instance.ProcessPlayerDead();
        }
    }
}
