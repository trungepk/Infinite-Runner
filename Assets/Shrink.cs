using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {

    Vector3 temp;
    private void Duck()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            temp = transform.localScale;

            Debug.Log(transform.localScale + " (before duck)");
            temp.y -= Time.deltaTime;
            transform.localScale = temp;
            Debug.Log(transform.localScale + " (after duck)");
        }else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.localScale = temp;
        }
        

    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Duck();
	}
}
