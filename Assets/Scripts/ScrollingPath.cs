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
        if (transform.position.z < -pathLength)
        {
            Repositioning();
            gameObject.SetActive(false);
        }
	}

    private void Repositioning()
    {
        Vector3 pathOffset = new Vector3(0, 0, pathLength * 2);
        string[] typesOfPath = { Constants.PathTag, Constants.CliffTag };
        int rnd = Random.Range(0, typesOfPath.Length);
        if (GameObject.FindGameObjectsWithTag(Constants.CliffTag).Length > 0) { rnd = 0; } // NOT spawn cliff if there is already one actives
        GameObject path = ObjectPooler.SharedInstance.GetPooledObject(typesOfPath[rnd]);
        if (path)
        {
            if (path.tag == Constants.CliffTag)
            {
                pathOffset = CaculateCliffOffset(pathOffset);
            }

            path.transform.position = Vector3.Scale(transform.position, new Vector3(0, 1, 1)) + pathOffset;
            path.transform.rotation = Quaternion.identity;
            path.SetActive(true);
        }
    }

    private static Vector3 CaculateCliffOffset(Vector3 pathOffset)
    {
        float[] offset = { -1.5f, 0, 1.5f };
        int rndOffset = Random.Range(0, offset.Length);
        pathOffset += new Vector3(offset[rndOffset], 0, 0);
        return pathOffset;
    }
}
