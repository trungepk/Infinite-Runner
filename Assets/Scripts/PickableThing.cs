using UnityEngine;

public class PickableThing : MonoBehaviour {

    [SerializeField] private int point = 1;
    [SerializeField] private int moneyValue = 1;

    public int Point { get { return point; } }
    public int MoneyValue { get { return moneyValue; } set { moneyValue = value; } }

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
            EventDispatcher.RaiseOnPickUp(this);

            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
