using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public void OnSelectItem()
    {
        string itemJson = Inventory.possessingItemJson[transform.GetSiblingIndex()];
        InventoryItemData item = JsonUtility.FromJson<InventoryItemData>(itemJson);
        InventoryManager.selectedItem = item;
    }
}
