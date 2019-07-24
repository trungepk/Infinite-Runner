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
        Debug.Log(PlayerPrefs.GetInt(Constants.Money));
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

    public void OnSelectItem()
    {
        GameObject selectedItemObj = EventSystem.current.currentSelectedGameObject;
        int selectedItemIndex = selectedItemObj.transform.GetSiblingIndex();
        ShopItem selectedItem = shopItems[selectedItemIndex];

        uiDisplay = FindObjectOfType<UIDisplay>();
        uiDisplay.DisplayItemInfo(selectedItem.itemName, selectedItem.itemDescription, selectedItem.cost);
    }
}
	
	

