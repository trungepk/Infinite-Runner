using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    [SerializeField] private AudioClip pickupCoin, collide;
    [SerializeField] [Range(0, 1)] float pickupCoinVolume = 1f;
    [SerializeField] [Range(0, 1)] float collideVolume = 1f;

    private void Start()
    {
        PickableThing.OnPickUp += TriggerPickupCoinSFX;
    }

    private void TriggerPickupCoinSFX(PickableThing pickable)
    {
        if (!pickupCoin) { return; }
        AudioSource.PlayClipAtPoint(pickupCoin, Camera.main.transform.position, pickupCoinVolume);
    }

    private void TriggerCollideSFX()
    {
        if (!collide) { return; }
        AudioSource.PlayClipAtPoint(collide, Camera.main.transform.position, collideVolume);
    }
}
