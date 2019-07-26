using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public static List<ShopItem> inventory = new List<ShopItem>();
    [SerializeField] private GameObject inventoryItemSlotPrefab;
    [SerializeField] private Transform inventoryItemContainer;

    private void Awake()
    {
        SaveSystem.Init();
    }
    private void Start()
    {
        ShopManager.instance.onBuyItemCallBack += AddItemToInventory;
        
    }

    private void AddItemToInventory()
    {
        inventory.Add(ShopManager.selectedItem);

        InventoryItemData data = new InventoryItemData(ShopManager.selectedItem);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        SaveSystem.SaveItem(json);

        string savedItemJson = SaveSystem.LoadItem();
        InventoryItemData savedItem = JsonUtility.FromJson<InventoryItemData>(savedItemJson);
        Debug.Log(savedItem.itemName);

        GameObject item = Instantiate(inventoryItemSlotPrefab, inventoryItemContainer);
        item.transform.GetChild(0).GetComponent<Image>().sprite = ShopManager.selectedItem.sprite;
    }

    public void OnSellItem()
    {
        Debug.Log(InventoryManager.selectedItem.itemName);
    }
}
