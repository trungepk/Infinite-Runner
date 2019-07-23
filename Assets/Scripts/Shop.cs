using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [Header("List of item sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    void Start() {
        PopulateShop();
    }

    private void PopulateShop()
    {
        for(var i = 0; i < shopItems.Length; i++)
        {
            ShopItem si = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.transform.GetChild(1).GetComponent<Text>().text = si.itemName;
            itemObject.transform.GetChild(2).GetComponent<Image>().sprite = si.sprite;
        }
    }
}
	
	

