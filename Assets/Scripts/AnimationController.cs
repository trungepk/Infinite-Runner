using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [System.NonSerialized] public float idleTime;
    [System.NonSerialized] public float shrinkingTime;
    [System.NonSerialized] public float swellingTime;

    public static AnimationController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateAnimClipLength();
    }

    private void UpdateAnimClipLength()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Idle":
                    idleTime = clip.length;
                    break;
                case "Shrinking":
                    shrinkingTime = clip.length;
                    break;
                case "Swelling":
                    swellingTime = clip.length;
                    break;
            }
        }
    }
}
