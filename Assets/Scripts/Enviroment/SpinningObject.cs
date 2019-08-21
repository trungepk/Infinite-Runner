using UnityEngine;

public class SpinningObject : MonoBehaviour {
    [SerializeField] private float spinSpeed = 5f;
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
	}
}
