using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    // Private Variables
    const float groundCheckRadius = 0.15f;
    const float overheadCheckRadius = 0.15f;

    Rigidbody2D rigidBody;
    Animator animator;
    [SerializeField]Collider2D standingCollider;
    [SerializeField]Transform overheadCheckCollider;
    [SerializeField]Transform groundCheckCollider;
    [SerializeField]LayerMask groundLayer;

    float horizontalValue;
    float jumpPower = 75;
    float runSpeedModifier = 3;
    float speedMultiplier = 100;

    bool facingRight = true;
    bool isRunning;
    bool isGrounded;
    bool jump;
    bool isDead;

    // Public Variables
    public float speed = 1;

    void Update(){
        if(CanMove() == false){ return; }
        horizontalValue = Input.GetAxisRaw("Horizontal");

        // Running
            // press Left Shift to run
        if(Input.GetKeyDown(KeyCode.LeftShift)){isRunning = true;}
        if(Input.GetKeyUp(KeyCode.LeftShift)){isRunning = false;}

        // Jump
            // press space bar to jump
        if(Input.GetButtonDown("Jump")){ 
            animator.SetBool("Jump", true); 
            jump = true;    
        }
        else if(Input.GetButtonUp("Jump")){ jump = false; }

        // Y Velocity
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
    }

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        GroundCheck();
        Move(horizontalValue, jump);
    }

    // When overlays are shown, restrict movement
    bool CanMove(){
        bool canMove = true;
        if(FindObjectOfType<InventorySystem>().isOpen){ canMove = false; }
        else if(FindObjectOfType<LawsDefinitions>().isOpen){ canMove = false; }
        else if(FindObjectOfType<PauseDisplay>().isPaused){ canMove = false; }
        if(isDead){ canMove = false; }
        return canMove;
    }

    // Check if player is on the ground
    public void GroundCheck(){
        isGrounded = false; // default, stops here, except when actually on ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0){
            isGrounded = true;
            animator.SetBool("Jump", !isGrounded);

            // Moving Platform Check, parent the platform to this
            foreach(var c in colliders){
                if(c.tag == "MovingPlatform"){
                    transform.parent = c.transform;
                }
            }
        }
        else{ transform.parent = null; }

        // If player is NOT grounded, and
        // If Y Velocity is greater than OR less than 0, set jump animation boolean to TRUE
        if(isGrounded == false){
            if(rigidBody.velocity.y > 1 || rigidBody.velocity.y < 0.1){ animator.SetBool("Jump", true); }
        }
    }

    void Move(float direction, bool jumpFlag){
        #region Jump
        if(isGrounded){
            // Jump
            if(jumpFlag){
                isGrounded = false;
                jumpFlag = false;
                rigidBody.AddForce(new Vector2(0f, jumpPower));
            }
        }
        #endregion

        #region Speed / Velocity
        // Speed
        float xVal = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        if(isRunning){ xVal *= runSpeedModifier; }
        Vector2 targetVelocity = new Vector2(xVal, rigidBody.velocity.y);
        rigidBody.velocity = targetVelocity;
        #endregion

        #region Sprite Flip and Walking / Running Animation
        // Sprite Flip
            // Right to Left
        if(facingRight && direction < 0){
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

            // Left to Right
        else if(!facingRight && direction > 0){
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

            // Walking/Running Animation
        animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        #endregion
    }

    public void Die(){
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }
}