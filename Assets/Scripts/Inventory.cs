using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public static List<string> possessingItemJson;
    public static List<string> possessingItemPaths;
    [SerializeField] private GameObject inventoryItemSlotPrefab;
    [SerializeField] private Transform inventoryItemContainer;
    [SerializeField] private Sprite[] iconSprites;
    [SerializeField] private GameObject player;
    [SerializeField] private Material[] playerSkin;

    private void Awake()
    {
        SaveSystem.Init();
    }
    private void Start()
    {
        EventDispatcher.OnBuyItem += AddItemToInventory;
        PopulatePossessingItem();
        possessingItemPaths = SaveSystem.ScanSaveFolder();
    }

    private void PopulatePossessingItem()
    {
        possessingItemJson = SaveSystem.LoadAllSavedItems();
        foreach(var json in possessingItemJson)
        {
            if (!String.IsNullOrEmpty(json))
            {
                InventoryItemData possessingItem = JsonUtility.FromJson<InventoryItemData>(json);
                GameObject item = Instantiate(inventoryItemSlotPrefab, inventoryItemContainer);
                item.transform.GetChild(0).GetComponent<Image>().sprite = GetIconSprite(possessingItem);
            }
        }
    }

    private void AddItemToInventory()
    {
        InventoryItemData data = new InventoryItemData(ShopManager.selectedItem);
        string json = JsonUtility.ToJson(data);
        SaveSystem.SaveItem(json);
        possessingItemJson.Add(json);
        possessingItemPaths = SaveSystem.ScanSaveFolder();

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
                return iconSprites[i];
            }
        }
        return null;
    }

    public void OnSellItem()
    {
        for(var i = 0; i < possessingItemJson.Count; i++)
        {
            if (possessingItemJson[i].Contains(InventoryManager.selectedItem.itemName))
            {
                File.WriteAllText(possessingItemPaths[i], "");
                Destroy(inventoryItemContainer.GetChild(i).gameObject);
                possessingItemJson.RemoveAt(i);
                possessingItemPaths = SaveSystem.ScanSaveFolder();
                break;
            }
        }

        int moneyAfterSelling = PlayerPrefs.GetInt(Constants.Money) + InventoryManager.selectedItem.value;
        PlayerPrefs.SetInt(Constants.Money, moneyAfterSelling);
        EventDispatcher.RaiseOnMoneyChanged();
    }

    public void UseItem()
    {
        player.GetComponent<Renderer>().material = GetMaterial(InventoryManager.selectedItem); //Temporary solution
    }

    private Material GetMaterial(InventoryItemData inventoryItem)
    {
        for (var i = 0; i < playerSkin.Length; i++)
        {
            if (inventoryItem.spriteName == playerSkin[i].name)
            {
                return playerSkin[i];
            }
        }
        return null;
    }
}
