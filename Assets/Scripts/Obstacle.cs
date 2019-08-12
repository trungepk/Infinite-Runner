using System;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private int damage = 1;

    public int Damage { get { return damage; } }

    public static event Action<Obstacle> OnCollideWithPlayer;

    private void OnEnable()
    {
        if(tag == Constants.HighObstacleTag)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constants.PlayerTag)
        {
            if (OnCollideWithPlayer != null)
                OnCollideWithPlayer(this);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.PlayerTag)
        {
            if (OnCollideWithPlayer != null)
                OnCollideWithPlayer(this);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
