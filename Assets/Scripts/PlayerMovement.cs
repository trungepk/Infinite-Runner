using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool canMove = true;

    private Animator anim;
    //[System.NonSerialized] public float idleTime;
    //[System.NonSerialized] public float shrinkingTime;
    //[System.NonSerialized] public float swellingTime;

    private float lerpTime = 1f;
    private float currentLerpTime;
    private Vector3 shrinkAndSwellScale;
    private Vector3 duckScale;
    private Vector3 originalScale;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //UpdateAnimClipLength();
        originalScale = transform.localScale;
        shrinkAndSwellScale = originalScale;
    }

    //private void UpdateAnimClipLength()
    //{
    //    AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;

    //    foreach (AnimationClip clip in clips)
    //    {
    //        switch (clip.name)
    //        {
    //            case "Idle":
    //                idleTime = clip.length;
    //                break;
    //            case "Shrinking":
    //                shrinkingTime = clip.length;
    //                break;
    //            case "Swelling":
    //                swellingTime = clip.length;
    //                break;
    //        }
    //    }
    //}
    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        bool isGrounded = rb.velocity.y <= 0 && rb.velocity.y >= -0.9 && transform.position.y < 2;
        if (Input.GetButtonDown("Jump") && isGrounded)
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
        Duck();
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

    //private IEnumerator GoLeft(float perc)
    //{
    //    canMove = false;
    //    anim.SetTrigger(Constants.ShrinkAnim);
    //    yield return new WaitForSeconds(shrinkingTime + 0.1f);
    //    Vector3 left = transform.position + Vector3.left;
    //    transform.position = Vector3.Lerp(transform.position, left, perc);
    //    anim.SetTrigger(Constants.SwellAnim);
    //    yield return new WaitForSeconds(swellingTime);
    //    canMove = true;
    //}

    //private IEnumerator GoRight(float perc)
    //{
    //    canMove = false;
    //    anim.SetTrigger(Constants.ShrinkAnim);
    //    yield return new WaitForSeconds(shrinkingTime + 0.1f);
    //    Vector3 right = transform.position + Vector3.right;
    //    transform.position = Vector3.Lerp(transform.position, right, perc);
    //    anim.SetTrigger(Constants.SwellAnim);
    //    yield return new WaitForSeconds(swellingTime);
    //    canMove = true;
    //}

    
    private IEnumerator GoLeft(float perc)
    {
        canMove = false;
        while (true)
        {
            shrinkAndSwellScale = transform.localScale;
            shrinkAndSwellScale.x -= Time.deltaTime * moveSpeed;
            shrinkAndSwellScale.z -= Time.deltaTime * moveSpeed;
            transform.localScale = shrinkAndSwellScale;
            yield return new WaitForEndOfFrame();
            if(shrinkAndSwellScale.x <= 0 ) { break; }
        }
        
        Vector3 left = transform.position + Vector3.left;
        transform.position = Vector3.Lerp(transform.position, left, perc);

        while (true)
        {
            shrinkAndSwellScale = transform.localScale;
            shrinkAndSwellScale.x += Time.deltaTime * moveSpeed;
            shrinkAndSwellScale.z += Time.deltaTime * moveSpeed;
            transform.localScale = shrinkAndSwellScale;
            yield return new WaitForEndOfFrame();
            if (shrinkAndSwellScale.x >= originalScale.x) { break; }
        }

        transform.localScale = originalScale;
        canMove = true;
    }
    private IEnumerator GoRight(float perc)
    {
        canMove = false;
        while (true)
        {
            shrinkAndSwellScale = transform.localScale;
            shrinkAndSwellScale.x -= Time.deltaTime * moveSpeed;
            shrinkAndSwellScale.z -= Time.deltaTime * moveSpeed;
            transform.localScale = shrinkAndSwellScale;
            yield return new WaitForEndOfFrame();
            if (shrinkAndSwellScale.x <= 0) { break; }
        }

        Vector3 right = transform.position + Vector3.right;
        transform.position = Vector3.Lerp(transform.position, right, perc);

        while (true)
        {
            shrinkAndSwellScale = transform.localScale;
            shrinkAndSwellScale.x += Time.deltaTime * moveSpeed;
            shrinkAndSwellScale.z += Time.deltaTime * moveSpeed;
            transform.localScale = shrinkAndSwellScale;
            yield return new WaitForEndOfFrame();
            if (shrinkAndSwellScale.x >= originalScale.x) { break; }
        }

        transform.localScale = originalScale;
        canMove = true;
    }

    
    private void Duck()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            duckScale = transform.localScale;
            duckScale.y /= 2f;
            transform.localScale = duckScale;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.localScale = originalScale;
        }
    }
}
