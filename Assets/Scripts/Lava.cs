using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] private float scrollingSpeed = 5f;
    private float pathLength;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.PlayerTag)
        {
            GameSession.instance.live = 0;
            GameSession.instance.ProcessPlayerDead();
            Destroy(other);

            if (GameSession.instance.onGetHitCallBack != null)
                GameSession.instance.onGetHitCallBack.Invoke();
        }
    }

    void Start()
    {
        pathLength = transform.localScale.z;
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * scrollingSpeed;
        if (transform.position.z < -pathLength * 3 / 4)
        {
            Repositioning();
            gameObject.SetActive(false);
        }
    }

    private void Repositioning()
    {
        Vector3 pathOffset = new Vector3(0, 0, pathLength * 2);
        GameObject lava = ObjectPooler.SharedInstance.GetPooledObject(Constants.LavaTag);
        if (lava)
        {
            lava.transform.position = Vector3.Scale(transform.position, new Vector3(0, 1, 1)) + pathOffset;
            lava.transform.rotation = transform.rotation;
            lava.SetActive(true);
        }
    }
}
