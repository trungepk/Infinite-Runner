using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    private void Start()
    {
    }

    public void OnSelectItem()
    {
        string itemJson = Inventory.possessingItemJson[transform.GetSiblingIndex()];
        InventoryItemData item = JsonUtility.FromJson<InventoryItemData>(itemJson);
        InventoryManager.selectedItem = item;
    }

    

}
