using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private Rigidbody2D rb;
    private Animator playerAnimator;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float airSpeed;
    [SerializeField]
    private float jumpForce;

    private bool attack;
    private bool upperCut;

    private bool facingRight;

    private bool isGrounded;
    private bool jump;

    [SerializeField]
    private GameObject upperCutHitBox;
    [SerializeField]
    private GameObject punchHitBox;

    private int juggleBonus;

    // Use this for initialization
    void Start ()
    {
        facingRight = false;
        attack = false;

        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(0, 1);
        Physics2D.IgnoreLayerCollision(1, 2);
	}

    private void Update()
    {
        GetInput();
    }
   

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Punch();

        Move(horizontal);

        Flip(horizontal);

        HandelLayers();

        Reset();
    }

    private void Move(float horizontal)
    {
        if (!upperCut)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontal * airSpeed, rb.velocity.y);
            }

            playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        if (isGrounded && jump && !upperCut)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }

    }
    private void Punch()
    {
        if(attack)
        {
            playerAnimator.SetTrigger("attack");
            if (facingRight)
            {
                Instantiate(punchHitBox, new Vector3(rb.position.x + 0.3f, rb.position.y + 0.1f, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(punchHitBox, new Vector3(rb.position.x + -0.3f, rb.position.y + 0.1f, 0), Quaternion.Euler(0, 0, 0));
            }
        }

        if (upperCut)
        {
            playerAnimator.SetTrigger("upperCut");
            if(facingRight)
            {
                Instantiate(upperCutHitBox, new Vector3(rb.position.x + 0.2f, rb.position.y + 0.5f, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(upperCutHitBox, new Vector3(rb.position.x + -0.2f, rb.position.y + 0.5f, 0), Quaternion.Euler(0, 0, 0));
            }
            
        }
        
    }
    public void GetInput()
    {
        if (Input.GetKey("down") && Input.GetKeyDown("z"))
        {
            upperCut = true;
        }
        else if (Input.GetKeyDown("z"))
        {
             attack = true;
        }

        if(Input.GetKeyDown("space"))
        {
            if (isGrounded)
            {
                jump = true;
                playerAnimator.SetTrigger("jump");
            }
        }
    }
    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }

    }

    private void HandelLayers()
    {
        if (!isGrounded)
        {
            playerAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            playerAnimator.SetLayerWeight(1, 0);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.y <= 0)
        {
            if (collision.transform.tag.Equals("Ground"))
            {
                if (!isGrounded)
                {
                    playerAnimator.SetBool("land", true);
                }

                isGrounded = true;

            }
            else if (collision.transform.tag.Equals("Button"))
            {
                if (!isGrounded)
                {
                    playerAnimator.SetBool("land", true);
                }

                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            
        }

    }

    public bool FacingRight()
    {
        if (facingRight) { return true; }
        else { return false; }
    }

    public void SetJuggleBonus(int bonus)
    {
        juggleBonus = bonus;
    }
    public int GetJuggleBonus()
    {
        return juggleBonus;
    }

    public void Reset()
    {
        attack = false;
        upperCut = false;
        jump = false;

        playerAnimator.SetBool("land", false);
    }

}
