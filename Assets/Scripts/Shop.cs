using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [Header("List of item sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private UIDisplay uiDisplay;

    void Start() {
        PopulateShop();
    }

    private void PopulateShop()
    {
        for(var i = 0; i < shopItems.Length; i++)
        {
            ShopItem si = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            //Assign shop item properties
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = si.sprite;
        }
        uiDisplay.DisplayItemInfo(shopItems[0].itemName, shopItems[0].itemDescription, shopItems[0].cost); //Default display
    }

    //Shop Item Object in Shop Container calls this method on clicked.
    public void OnSelectItem()
    {
        GameObject selectedItemObj = EventSystem.current.currentSelectedGameObject;
        int selectedItemIndex = selectedItemObj.transform.GetSiblingIndex();
        ShopManager.selectedItem = shopItems[selectedItemIndex];
        Debug.Log(ShopManager.selectedItem.name);

        uiDisplay = FindObjectOfType<UIDisplay>();
        uiDisplay.DisplayItemInfo(ShopManager.selectedItem.itemName, ShopManager.selectedItem.itemDescription, ShopManager.selectedItem.cost);
    }

    //Buy button calls this method on clicked
    public void OnBuyItem()
    {
        if (ShopManager.selectedItem)
        {
            Debug.Log(ShopManager.selectedItem.cost);
        }
        else
        {
            Debug.Log("No item selected");
            return;
        }

        int moneyAfterPurchase = PlayerPrefs.GetInt(Constants.Money) - ShopManager.selectedItem.cost;
        PlayerPrefs.SetInt(Constants.Money, moneyAfterPurchase);
        uiDisplay.DisplayMoneyAfterPurchase();
    }
}
	
	

