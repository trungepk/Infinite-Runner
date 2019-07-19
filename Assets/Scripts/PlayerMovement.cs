using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool canMove = true;

    private Animator anim;
    [System.NonSerialized] public float idleTime;
    [System.NonSerialized] public float shrinkingTime;
    [System.NonSerialized] public float swellingTime;

    private float lerpTime = 1f;
    private float currentLerpTime;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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
    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    void Update()
    {
        MoveSideWay();
    }

    private void MoveSideWay()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float perc = currentLerpTime / lerpTime;

        if (Input.GetKeyDown(KeyCode.RightArrow) && canMove)
        {
            StartCoroutine(GoRight(perc));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
        {
            StartCoroutine(GoLeft(perc));
        }
        transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, transform.position.z);
    }

    private IEnumerator GoLeft(float perc)
    {
        canMove = false;
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
        canMove = false;
        anim.SetTrigger("Shrink");
        yield return new WaitForSeconds(shrinkingTime + 0.1f);
        Vector3 right = transform.position + Vector3.right;
        transform.position = Vector3.Lerp(transform.position, right, perc);
        anim.SetTrigger("Swell");
        yield return new WaitForSeconds(swellingTime);
        canMove = true;
    }
}
