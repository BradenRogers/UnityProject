using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    //Unit unit;

    /* Components */
    private Animator anim;
    private SpriteRenderer spriteRend;

    /* Movements */
    [Header("Movements")]
    public float jumpForce = 400; 
    [SerializeField] public float jumpForceMax = 450f;
    [SerializeField] private float jumpIncrese = 0.01f;
    private float originalJumpForce;
    private bool isJumping = false;
    [SerializeField] private float movementSpeed = 40.0f;

    private Vector3 velocity = Vector3.zero;
    [Range(0,0.3f), SerializeField] private float movementSmoothing = 0.5f;
    private float horizontalMovement = 0f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer = 1<<0;
    [SerializeField] private float rayCastLength = 5f;


    /* Inventory */
    [SerializeField] private Inventory inventory;
    
    /* Other */
    private bool isInIFrames = false;
    private float maxHealth;

    Player()
    {
        //unit = new Unit(100,100);
    }

    private void Start()
    {
        gameManager.GetUI().UpdateCoinUI(inventory.GetPickUpables().Count);
    }

    protected override void UnitAwake()
    {
        originalJumpForce = jumpForce;
        maxHealth = health;

        //Get Components
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public override void ApplyDamage(float inDamage)
    {
        // I Frame handling
        if(!isInIFrames)
        {
            // apply the damage and updates the UI
            health -= inDamage;
            gameManager.GetUI().UpdateHealthBar();
            Debug.Log("Damage Taken: " + inDamage + ". Health remaining: " + health);

            if(health <= 0)
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

        // Puts player into IFrames
        isInIFrames = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerIFrame");

        // Flashes player to show damage
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
        
        // Takes player out of IFrame
        gameObject.layer = LayerMask.NameToLayer("Player");
        isInIFrames = false;
        yield break;
    }

    private void FixedUpdate()
    {
        // Movement(horizontalMovement * Time.fixedDeltaTime); moved to update becuase jump doesnt always work in fixed update
    }

    private void Update()
    {   
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        Movement(horizontalMovement * Time.fixedDeltaTime);
    }

    //move this to another script later
    private void Movement(float horAxis)
    {
        // Player movements
        if(horAxis > 0 || horAxis < 0)
        {
            Vector3 targetVelocity = new Vector2(horAxis * 10f, rB.velocity.y);
		    rB.velocity = Vector3.SmoothDamp(rB.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        else
        {
            // sets veleocity to 0 to stop player from slidding
            rB.velocity = new Vector2(0, rB.velocity.y);
        }
        
        //Set animation value
        anim.SetFloat("movementSpeed", Mathf.Abs(rB.velocity.x));

        //Flipping the sprite
        if(horAxis > 0)
        {
           spriteRend.flipX = false;
        }
        else if (horAxis < 0)
        {
            spriteRend.flipX = true;
        }

        // Jump handling
        if(!isJumping)
        {
            if(Input.GetButtonUp("Jump")) // using GetButtonUp might cause problems with not jumping when space is clicked
            {   
                // jumps when space is let go off
                rB.velocity = (Vector2.up * jumpForce);
                jumpForce = originalJumpForce;
                isJumping = true;
            }
            else if(Input.GetButton("Jump"))
            {
                // increase jump force while holding jump
                jumpForce = Mathf.LerpUnclamped(jumpForce, jumpForceMax, jumpIncrese);
                anim.SetBool("isJumping", true);   
                // Update UI
                gameManager.GetUI().UpdateJumpBar();
            }

            
        }
        
        if(rB.velocity.y < 0 && isJumping)
        {
            // smooths out the falling of a jump making a parabola
            rB.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
            anim.SetBool("isJumping", false);
            anim.SetBool("isLanding", true);

            if(Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, groundLayer))
            {
                anim.SetBool("isLanding", false);
                isJumping = false; 
                gameManager.GetUI().UpdateJumpBar();
            }
        }
    }

    public void Heal(float inHealth)
    {
        // heals
        health += inHealth;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        // Update UI
        gameManager.GetUI().UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // item pickup
        IInventoryItem item = otherCollider.gameObject.GetComponent<IInventoryItem>();
        if(item != null)
        {
            inventory.AddItem(item);
            gameManager.GetUI().UpdateCoinUI(inventory.GetPickUpables().Count);
        }
    }
}
