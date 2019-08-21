using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    #region Singleton
    private void Awake()
    {
        if(SharedInstance == null)
        {
            SharedInstance = this;
        }else if(SharedInstance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private List<ObjectPoolItem> itemToPool;
    private List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach(var item in itemToPool)
        {
            for (var i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                return pooledObjects[i];
        }

        foreach(var item in itemToPool)
        {
            if(item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
