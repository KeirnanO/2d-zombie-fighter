using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform zombie;

    public Player player;

    private int health = 3;
    private int delayTime = 0;
    private Vector2 moveSpeed = new Vector2(1,-1);

    private float isPunched;

    private bool isGrounded;
    private int juggleBonus;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreLayerCollision(0, 0);
    }

    private void Update()
    {
        delayTime--;

        player.SetJuggleBonus(juggleBonus);

    }

    // Use this for initialization
    void FixedUpdate()
    {
        if (isGrounded && delayTime <= 0)
        {
            rb.velocity = moveSpeed;
        }
        ResetValues();

	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag.Equals("Ground"))
        {
            isGrounded = true;
            juggleBonus = 0;
        }
    }

    public void ResetValues()
    {
        
    }

    public void UpperCut(int face)
    {
        isGrounded = false;
        juggleBonus++;
        zombie.Translate(new Vector3(0, 1, 0));
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(new Vector2(100 * face, 500));
    }
    public void Punch(int face)
    {
        if(isGrounded)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector2((175 * face), -1));
            StartDelayTimer(50);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector2((300 * face),-1));
            juggleBonus++;
        }
        isPunched = rb.velocity.x;

    }

    public void StartDelayTimer(int time)
    {
        delayTime = time;
    }



}
