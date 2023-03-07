using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKickDetection : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    public float backwardKnockBack;
    public float upwardKnockBack;
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        }
    }
}
