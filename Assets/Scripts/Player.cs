﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    Unit unit;

    /* Components */
    private Animator anim;
    private SpriteRenderer spriteRend;

    /* Movements */
    [Header("Movements")]
    [SerializeField] private float jumpForce = 400; 
    [SerializeField] private float jumpForceMax = 450f;
    [SerializeField] private float jumpIncrese = 0.01f;
    private float originalJumpForce;
    private bool isJumping = false;
    [SerializeField] private float movementSpeed = 40.0f;
    [SerializeField] private float sprintMultiplyer = 2.0f;

    private Vector3 velocity = Vector3.zero;
    [Range(0,0.3f), SerializeField] private float movementSmoothing = 0.5f;
    private float horizontalMovement = 0f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask ground = 1<<0;
    [SerializeField] private float rayCastLength = 5f;

    /* Other */
    private bool isInIFrames = false;

    Player()
    {
        unit = new Unit(100,100);
    }

    void Start()
    {
        originalJumpForce = jumpForce;
    }

    private void Awake()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public override void ApplyDamage(float inDamage)
    {
        if(!isInIFrames)
        {
            Health -= inDamage;
            if(Health <= 0)
            {
                Death();
            }
            else
            {
                StartCoroutine(TakeDamage());
            }
        
        }
    }

    private IEnumerator TakeDamage()
    {
        //Colours 
        Color transparent = new Color(1,1,1,0.2f);
        Color normal = new Color(1,1,1,1);
        Color almostNormal = new Color(1,1,1,0.7f);

        float timeToWait = 0.2f;

        isInIFrames = true;
        
        spriteRend.color = transparent;
        yield return new WaitForSecondsRealtime(timeToWait);
        spriteRend.color = almostNormal;
        yield return new WaitForSecondsRealtime(timeToWait);
        spriteRend.color = transparent;
        yield return new WaitForSecondsRealtime(timeToWait);
        spriteRend.color = almostNormal;
        yield return new WaitForSecondsRealtime(timeToWait);
        spriteRend.color = transparent;
        yield return new WaitForSecondsRealtime(timeToWait);
        spriteRend.color = normal;
        
        isInIFrames = false;
        yield break;
    }

    private void FixedUpdate()
    {
        Movement(horizontalMovement * Time.fixedDeltaTime);
    }

    private void Update()
    {   
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
    }

    //move this to another script
    private void Movement(float horAxis)
    {
        Vector3 targetVelocity = new Vector2(horAxis * 10f, rB.velocity.y);
		rB.velocity = Vector3.SmoothDamp(rB.velocity, targetVelocity, ref velocity, movementSmoothing);

        float speed = rB.velocity.x;
        
        anim.SetFloat("movementSpeed", Mathf.Abs(speed));

        if(horAxis > 0)
        {
           spriteRend.flipX = false;
        }
        else if (horAxis < 0)
        {
            spriteRend.flipX = true;
        }

        if(!isJumping)
        {
            if(Input.GetButton("Jump"))
            {
                jumpForce = Mathf.Lerp(jumpForce, jumpForceMax, jumpIncrese);  
            }

            if(Input.GetButtonUp("Jump"))
            {   
                rB.velocity = (Vector2.up * jumpForce);
                jumpForce = originalJumpForce;
                anim.SetBool("isJumping", true); 
                isJumping = true;
            }
        }
        
        if(rB.velocity.y < 0 && isJumping)
        {
            rB.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
            anim.SetBool("isJumping", false);
            anim.SetBool("isLanding", true);

            if(Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, ground))
            {
                anim.SetBool("isLanding", false);
                isJumping = false; 
            }
        }
    }
}