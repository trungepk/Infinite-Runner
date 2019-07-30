﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volumn;
    [Range(-3f, 3f)] public float pitch;
    public bool loop;
    [HideInInspector] public AudioSource source;
}
