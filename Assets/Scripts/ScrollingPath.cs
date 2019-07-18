using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPath : MonoBehaviour {

    [SerializeField] private float scrollingSpeed = 5f;

    private BoxCollider pathCollider;
    private float pathLength;

    public float ScrollingSpeed{ get { return scrollingSpeed; } }

    void Start () {
        //pathCollider = GetComponent<BoxCollider>();
        pathLength = transform.localScale.z;
	}
	
	void Update () {
        transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * ScrollingSpeed;
        if(transform.position.z < -pathLength)
        {
            Repositioning();
        }
	}

    private void Repositioning()
    {
        Vector3 pathOffset = new Vector3(0, 0, pathLength * 2);
        transform.position += pathOffset;
    }
}
