using UnityEngine;

public class Laser : MonoBehaviour {
    //[SerializeField] private Camera cam;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform firePoint;

    void Update () {
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, new Vector3(-firePoint.position.x, firePoint.position.y, firePoint.position.z));
        if (firePoint.position.z <= -15) { gameObject.SetActive(false); }
	}

    private void OnEnable()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

}
