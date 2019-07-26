using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public void OnSelectItem()
    {
        InventoryManager.selectedItem = Inventory.inventory[gameObject.transform.GetSiblingIndex()];
    }
}
