using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<ShopItem> inventory;
    [SerializeField] private GameObject inventoryItemSlotPrefab;
    [SerializeField] private Transform inventoryItemContainer;
    private void Start()
    {
        ShopManager.instance.onBuyItemCallBack += AddItemToInventory;
    }

    private void AddItemToInventory()
    {
        inventory.Add(ShopManager.selectedItem);
        GameObject item = Instantiate(inventoryItemSlotPrefab, inventoryItemContainer);

        item.transform.GetChild(0).GetComponent<Image>().sprite = ShopManager.selectedItem.sprite;
    }

    private void PopulateInventory()
    {
        
    }
}
