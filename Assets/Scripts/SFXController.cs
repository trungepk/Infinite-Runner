using UnityEngine;

public class SFXController : MonoBehaviour {

    [SerializeField] private AudioClip pickupCoin, collide, lose;
    [SerializeField] [Range(0, 1)] float pickupCoinVolume = 1f;
    [SerializeField] [Range(0, 1)] float collideVolume = 1f;
    [SerializeField] [Range(0, 1)] float loseVolume = 1f;

    private void Start()
    {
        EventDispatcher.Subscribe(EventID.OnPickedUp, TriggerPickupCoinSFX);
        EventDispatcher.Subscribe(EventID.OnCollideWithPlayer, TriggerCollideSFX);
        EventDispatcher.Subscribe(EventID.OnLoseGame, TriggerLoseSFX);
    }

    private void OnDisable()
    {
        EventDispatcher.Unsubscribe(EventID.OnPickedUp, TriggerPickupCoinSFX);
        EventDispatcher.Unsubscribe(EventID.OnCollideWithPlayer, TriggerCollideSFX);
        EventDispatcher.Unsubscribe(EventID.OnLoseGame, TriggerLoseSFX);
    }

    private void TriggerPickupCoinSFX(object obj)
    {
        if (!pickupCoin) { return; }
        AudioSource.PlayClipAtPoint(pickupCoin, Camera.main.transform.position, pickupCoinVolume);
    }

    private void TriggerCollideSFX(object obj)
    {
        if (!collide) { return; }
        AudioSource.PlayClipAtPoint(collide, Camera.main.transform.position, collideVolume);
    }

    private void TriggerLoseSFX()
    {
        if (!lose) { return; }
        AudioSource.PlayClipAtPoint(lose, Camera.main.transform.position, loseVolume);
    }
}
