using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private int damage = 1;

    public int Damage { get { return damage; } }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " hit");
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            GameSession.instance.ProcessPlayerDead();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " hit");
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            GameSession.instance.ProcessPlayerDead();
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
