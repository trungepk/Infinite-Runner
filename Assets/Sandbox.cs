using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour {
    [SerializeField] private GameObject[] objs;
    private GameObject selectedObj;

    private void Start()
    {
    }

    public void SelectObj()
    {
        int rnd = Random.Range(0, objs.Length);
        selectedObj = objs[rnd];
        Debug.Log("Select " + selectedObj.name);
    }

    public void PrintSelectedObj()
    {
        if(selectedObj)
            Debug.Log("Print " + selectedObj.name);
        else
            Debug.Log("No item selected");
    }
}
