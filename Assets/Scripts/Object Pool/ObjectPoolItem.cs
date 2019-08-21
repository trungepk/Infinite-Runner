using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {

    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}
