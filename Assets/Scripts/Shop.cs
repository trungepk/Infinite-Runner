using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [Header("List of item sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Sprite soldOutSprite;

    private void Start()
    {
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
            if (!si.isAvailable)
            {
                itemObject.transform.GetChild(1).GetComponent<Image>().sprite = soldOutSprite;
            }
        }
    }

    //Shop Item Object in Shop Container calls this method on clicked.
    public void OnSelectItem()
    {
        GameObject selectedItemObj = EventSystem.current.currentSelectedGameObject;
        int selectedItemIndex = selectedItemObj.transform.GetSiblingIndex();
        ShopManager.selectedItem = shopItems[selectedItemIndex];
        ShopItem item = ShopManager.selectedItem;

        EventDispatcher.RaiseOnSelectItem(item.itemName, item.itemDescription, item.cost);
    }

    //Buy button calls this method on clicked
    public void OnBuyItem()
    {

        if (!ShopManager.selectedItem)
        {
            Debug.Log("No item selected");
            return;
        }
        if (ShopManager.selectedItem.isAvailable)
        {
            if (ShopManager.instance.HasMoney)
            {
                BuyItem();
            }
            else
            {
                Debug.LogWarning("Display not-enough-money UI");
            }
        }
    }

    private void BuyItem()
    {
        SubstractMoney();
        ChangeSoldOutSprite();
        ShopManager.selectedItem.isAvailable = false;
        EventDispatcher.RaiseOnBuyItem();
        ShopManager.selectedItem = null;
    }

    private void SubstractMoney()
    {
        int moneyAfterPurchase = PlayerPrefs.GetInt(Constants.Money) - ShopManager.selectedItem.cost;
        PlayerPrefs.SetInt(Constants.Money, moneyAfterPurchase);
        EventDispatcher.RaiseOnMoneyChanged();
    }

    private void ChangeSoldOutSprite()
    {
        for (var i = 0; i < shopContainer.childCount; i++)
        {
            Transform itemObj = shopContainer.GetChild(i);
            if (itemObj.GetSiblingIndex() == GetSelectedItemIndex())
            {
                itemObj.GetChild(1).GetComponent<Image>().sprite = soldOutSprite;
                break;
            }
        }
    }

    private int GetSelectedItemIndex()
    {
        for(var i = 0; i < shopItems.Length; i++)
        {
            if (ShopManager.selectedItem.itemName == shopItems[i].itemName)
            {
                return i;
            }
        }
        return -1;
    }
}
	
	

