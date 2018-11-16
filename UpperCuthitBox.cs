using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCuthitBox : MonoBehaviour {

    private float time = 0;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        time++;

        if(time == 16)
        {
            Physics.IgnoreLayerCollision(0, 2, false);
        }
        else
        {
            Physics.IgnoreLayerCollision(0, 2);
        }



        if (time >= 24)
        {
            Destroy(gameObject);
            Physics2D.IgnoreLayerCollision(0, 2, false);
        }
       

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if(collision.transform.tag.Equals("Enemy"))
        {
            Zombie enemy = collision.gameObject.GetComponent<Zombie>();

            if (time >= 16)
            {
                Physics2D.IgnoreLayerCollision(0, 2);
                if (player.FacingRight())
                {
                    enemy.UpperCut(1);
                }
                else
                {
                    enemy.UpperCut(-1);
                }

            }
        }

    }





}
