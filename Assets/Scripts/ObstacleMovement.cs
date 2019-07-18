using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {
    [SerializeField] private ScrollingPath path;
    private float speed;

    private Vector3 playerDir;

    private void Start()
    {
        speed = path.ScrollingSpeed;
        playerDir = Vector3.back;
    }
    private void Update()
    {
        transform.position += playerDir * Time.deltaTime * speed;
    }
}
