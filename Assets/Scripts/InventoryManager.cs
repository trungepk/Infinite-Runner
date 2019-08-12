using UnityEngine;

public class InventoryManager : MonoBehaviour {
    #region Singleton
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public static InventoryItemData selectedItem;

    public delegate void OnInteractWithItem();
    public OnInteractWithItem onSellItemCallBack;
}
