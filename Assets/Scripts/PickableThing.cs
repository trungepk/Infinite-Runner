using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableThing : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private int point = 1;

    public int Point { get { return point; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameSession.instance.AddPoint(point);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
