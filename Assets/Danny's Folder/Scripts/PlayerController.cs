using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //floats
    public float moveSpeed;
    public float jumpForce;
    private float horizontalInput;

    //Rigidbodys
    private Rigidbody2D playerRb;

    //KeyCodes
    public KeyCode jumpKey;
    public KeyCode attackKey;

    //Bools
    private bool canJump = true;
    private bool canAttack = true;

    //GameObjects
    public GameObject attackHitBox;

    //Tranforms
    public Transform attackHitBoxPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //left and right movement
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);
        }

        //jumping code. it sets the players velecity to zero so that you can't use the velecity from your prevous jump to rocket yourself up a wall
        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }

        if (Input.GetKeyDown(attackKey))
        {
            Instantiate(attackHitBox, attackHitBoxPosition.transform.position, attackHitBoxPosition.transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player can jump again when they touch the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
