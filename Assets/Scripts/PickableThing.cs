using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableThing : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private int point = 1;

    [SerializeField] private AudioClip coinPickUpSFX;

    public int Point { get { return point; } }

    private void OnEnable()
    {
        if(tag == Constants.CoinTag)
        {
            transform.Rotate(90, 0, 0); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PlayerTag)
        {
            GameSession.instance.AddPoint(point);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
