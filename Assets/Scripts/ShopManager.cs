using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    #region Singleton
    public static ShopManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public static ShopItem selectedItem;
    public bool HasMoney { get { return PlayerPrefs.GetInt(Constants.Money) > selectedItem.cost; } }

    public delegate void OnBuyItem();
    public OnBuyItem onBuyItemCallBack;

}
