using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    [System.NonSerialized] public float idleTime;
    [System.NonSerialized] public float shrinkingTime;
    [System.NonSerialized] public float swellingTime;

    [SerializeField] Animator anim;

	void Start () {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        Debug.Log(clips[0]);

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Idle":
                    idleTime = clip.length;
                    break;
                case "Shrinking":
                    shrinkingTime = clip.length;
                    Debug.Log(shrinkingTime);
                    break;
                case "Swelling":
                    swellingTime = clip.length;
                    break;
            }
        }
    }
}
