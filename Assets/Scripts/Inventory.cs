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
    [SerializeField] private Sprite[] iconSprites;

    private void Awake()
    {
        SaveSystem.Init();
    }
    private void Start()
    {
        ShopManager.instance.onBuyItemCallBack += AddItemToInventory;
        PopulatePossessingItem();
    }

    private void PopulatePossessingItem()
    {
        List<string> possessingItemJson = SaveSystem.LoadAllSavedItems();
        foreach(var json in possessingItemJson)
        {
            InventoryItemData possessingItem = JsonUtility.FromJson<InventoryItemData>(json);
            GameObject item = Instantiate(inventoryItemSlotPrefab, inventoryItemContainer);
            item.transform.GetChild(0).GetComponent<Image>().sprite = GetIconSprite(possessingItem);
        }
    }

    private void AddItemToInventory()
    {
        inventory.Add(ShopManager.selectedItem);

        InventoryItemData data = new InventoryItemData(ShopManager.selectedItem);
        string json = JsonUtility.ToJson(data);
        SaveSystem.SaveItem(json);

        string savedItemJson = SaveSystem.LoadItem();
        InventoryItemData savedItem = JsonUtility.FromJson<InventoryItemData>(savedItemJson);

        GameObject item = Instantiate(inventoryItemSlotPrefab, inventoryItemContainer);
        item.transform.GetChild(0).GetComponent<Image>().sprite = GetIconSprite(savedItem);
    }

    private Sprite GetIconSprite(InventoryItemData inventoryItem)
    {
        for (var i = 0; i < iconSprites.Length; i++)
        {
            if (inventoryItem.spriteName == iconSprites[i].name)
            {
                Debug.Log(i + " " + iconSprites[i]);
                return iconSprites[i];
            }
        }
        return null;
    }

    public void OnSellItem()
    {
        Debug.Log(InventoryManager.selectedItem.itemName);
    }
}
