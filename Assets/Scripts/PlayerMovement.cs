using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float sidewaySpeed = 5f;
    [SerializeField] private float timeDelta = 0.05f;
    private bool canMove = true;

    private Animator anim;
    [System.NonSerialized] public float idleTime;
    [System.NonSerialized] public float shrinkingTime;
    [System.NonSerialized] public float swellingTime;

    float lerpTime = 1f;
    float currentLerpTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateAnimClipLength();
    }

    private void UpdateAnimClipLength()
    {
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

    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float perc = currentLerpTime / lerpTime;

        if (Input.GetKeyDown(KeyCode.RightArrow) && canMove)
        {
            canMove = false;
            StartCoroutine(GoRight(perc));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
        {
            canMove = false;
            StartCoroutine(GoLeft(perc));
        }
    }

    private IEnumerator GoLeft(float perc)
    {
        anim.SetTrigger("Shrink");
        yield return new WaitForSeconds(shrinkingTime + 0.1f);
        Vector3 left = transform.position + Vector3.left;
        transform.position = Vector3.Lerp(transform.position, left, perc);
        anim.SetTrigger("Swell");
        yield return new WaitForSeconds(swellingTime);
        canMove = true;
    }

    private IEnumerator GoRight(float perc)
    {
        anim.SetTrigger("Shrink");
        yield return new WaitForSeconds(shrinkingTime + 0.1f);
        Vector3 right = transform.position + Vector3.right;
        transform.position = Vector3.Lerp(transform.position, right, perc);
        anim.SetTrigger("Swell");
        yield return new WaitForSeconds(swellingTime);
        canMove = true;
    }
}
