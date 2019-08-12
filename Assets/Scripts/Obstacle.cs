using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private int damage = 1;

    [SerializeField] private AudioClip collideSFX;

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
            Hit(damage);
            gameObject.SetActive(false);
            GameSession.instance.ProcessPlayerDead();

            if (GameSession.instance.onGetHitCallBack != null)
                GameSession.instance.onGetHitCallBack.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.PlayerTag)
        {
            Hit(damage);
            gameObject.SetActive(false);
            GameSession.instance.ProcessPlayerDead();

            if (GameSession.instance.onGetHitCallBack != null)
                GameSession.instance.onGetHitCallBack.Invoke();
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void Hit(int dmg)
    {
        GameSession.instance.live -= dmg;
        AudioSource.PlayClipAtPoint(collideSFX, Camera.main.transform.position);
    }
}
