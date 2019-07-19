using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPath : MonoBehaviour {

    [SerializeField] private float scrollingSpeed = 5f;

    private float pathLength;

    public float ScrollingSpeed{ get { return scrollingSpeed; } }

    void Start () {

        pathLength = transform.localScale.z;
	}
	
	void Update () {
        transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * ScrollingSpeed;
        if(transform.position.z < -pathLength)
        {
            Repositioning();
            gameObject.SetActive(false);
        }
	}

    private void Repositioning()
    {
        Vector3 pathOffset = new Vector3(0, 0, pathLength * 2);
        string[] typesOfPath = { "Path", "Cliff" };
        int rnd = Random.Range(0, typesOfPath.Length);
        if (GameObject.FindGameObjectsWithTag("Cliff").Length > 0) { rnd = 0; }
        GameObject path = ObjectPooler.SharedInstance.GetPooledObject(typesOfPath[rnd]);
        if (path)
        {
            if(path.tag == "Cliff")
            {
                float[] offset = { -1.5f, 0, 1.5f };
                int rndOffset = Random.Range(0, offset.Length);
                pathOffset += new Vector3(offset[rndOffset], 0, 0);
            }
            
            path.transform.position = Vector3.Scale(transform.position, new Vector3(0,1,1)) + pathOffset;
            path.transform.rotation = Quaternion.identity;
            path.SetActive(true);
        }
    }
}
