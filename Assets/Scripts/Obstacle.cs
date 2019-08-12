using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private int damage = 1;

    public int Damage { get { return damage; } }

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
            EventDispatcher.RaiseOnCollideWithPlayer(this);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.PlayerTag)
        {
            EventDispatcher.RaiseOnCollideWithPlayer(this);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
