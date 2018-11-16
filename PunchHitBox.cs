using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHitBox : MonoBehaviour {

    private Player player;
    private float time = 0;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        time++;


        if (time >= 24)
        {
            Destroy(gameObject);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.transform.tag.Equals("Enemy"))
        {
            Zombie enemy = collision.gameObject.GetComponent<Zombie>();

                if(player.FacingRight())
                {
                    enemy.Punch(1);
                }
                else
                {
                    enemy.Punch(-1);
                }

            Destroy(gameObject);
        }

    }
}
