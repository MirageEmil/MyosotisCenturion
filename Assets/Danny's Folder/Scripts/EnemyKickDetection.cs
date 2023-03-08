using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKickDetection : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    public float backwardKnockBack;
    public float upwardKnockBack;
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //depending on which side the player is on. it will knock the enemy back in the opposite direction
         if (collision.gameObject.CompareTag("Kick"))
         {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                enemyRb.AddForce(Vector2.left * backwardKnockBack, ForceMode2D.Impulse);
            }
            else
            {
                enemyRb.AddForce(Vector2.right * backwardKnockBack, ForceMode2D.Impulse);
            }
            enemyRb.AddForce(Vector2.up * upwardKnockBack, ForceMode2D.Impulse);

            //this is so that the kick can't hit the enemy multiple times
            collision.enabled = false;
         }
    }

    //This is so that the enemy does not slide really far after getting hit with a kick
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enemyRb.velocity = Vector2.zero;
        }
        
    }
}
