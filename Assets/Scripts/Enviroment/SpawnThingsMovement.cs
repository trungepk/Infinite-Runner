﻿using UnityEngine;

public class SpawnThingsMovement : MonoBehaviour {
    [SerializeField] private ScrollingPath path;
    private float speed;

    private Vector3 playerDir;

    private void OnEnable()
    {
        speed = path.ScrollingSpeed;
        playerDir = Vector3.back;
    }
    private void Update()
    {
        transform.position += playerDir * Time.deltaTime * speed;
    }
}