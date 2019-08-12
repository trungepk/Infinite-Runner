using System.Collections;
using UnityEngine;

public class SFXController : MonoBehaviour {

    [SerializeField] private AudioClip pickupCoin, collide, lose;
    [SerializeField] [Range(0, 1)] float pickupCoinVolume = 1f;
    [SerializeField] [Range(0, 1)] float collideVolume = 1f;
    [SerializeField] [Range(0, 1)] float loseVolume = 1f;

    private void Start()
    {
        EventDispatcher.OnPickUp += TriggerPickupCoinSFX;
        EventDispatcher.OnCollideWithPlayer += TriggerCollideSFX;
        //EventDispatcher.OnLoseGame += TriggerLoseSFX;
    }

    private void OnDisable()
    {
        EventDispatcher.OnPickUp -= TriggerPickupCoinSFX;
        EventDispatcher.OnCollideWithPlayer -= TriggerCollideSFX;
        //EventDispatcher.OnLoseGame -= TriggerLoseSFX;
    }

    private void TriggerPickupCoinSFX(PickableThing pickable)
    {
        if (!pickupCoin) { return; }
        AudioSource.PlayClipAtPoint(pickupCoin, Camera.main.transform.position, pickupCoinVolume);
    }

    private void TriggerCollideSFX(Obstacle obstacle)
    {
        if (!collide) { return; }
        AudioSource.PlayClipAtPoint(collide, Camera.main.transform.position, collideVolume);
    }

    private IEnumerator TriggerLoseSFX()
    {
        if (lose)
        {
            yield return null;
            AudioSource.PlayClipAtPoint(lose, Camera.main.transform.position, loseVolume);
        }
    }
}
