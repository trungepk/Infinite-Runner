[System.Serializable]
public class InventoryItemData {

    public string itemName;
    public int value;
    public string spriteName;
    public string itemDesription;
    public bool isUsing;

    public InventoryItemData(ShopItem item)
    {
        itemName = item.itemName;
        value = item.cost;
        spriteName = item.sprite.name;
        itemDesription = item.itemDescription;
        isUsing = false;
    }
}
