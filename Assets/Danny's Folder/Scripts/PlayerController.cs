using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Floats
    public float enemyMeleeDamage = 25f;
    public float enemyRangeDamage = 15f;
        //how long the kick hitbox is out
    public float kickTime;
        //how long after the kick hitbox is out should we wait until we can kick again
    public float kickCoolDown;

    public float shootCooldown;

    public float moveSpeed;
    public float jumpForce;
    private float horizontalInput;

    //Rigidbodys
    private Rigidbody2D playerRb;

    //KeyCodes. that way you can choose whatever key you like! maybe make it so this is changeable in an options menu?
    public KeyCode jumpKey;
    public KeyCode meleeAttackKey;
    public KeyCode rangedAttackKey;

    //Bools
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isMoving = false;
        //This bool can be used for the animator. this bool also allows our player to knock enemies left and right depending on which way they are facing
    private bool isFacingLeft = false;

    public bool shootingUnlocked = false;
    public bool canShoot = true;

    //BoxCollider2D
    public BoxCollider2D attackHitBox;

    //Sprite Renderer
    public SpriteRenderer attackSprite;

    //Scripts
    FollowScript kickFollowPlayerScript;

    //GameObjects
    public GameObject leftAttackPosition;
    public GameObject rightAttackPosition;

    public GameObject bullet;
    public GameObject placeToSpawnBullets;

    //Scripts
    public GameManager gm;

    //animator
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        attackHitBox.enabled = false;
        attackSprite.enabled = false;
        playerRb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();

            //this is for changing where to place the kick hitbox for when the player turns left or right
            /*kickFollowPlayerScript may be causing issues as there is no script that I can find(R)*/
        kickFollowPlayerScript = GameObject.FindGameObjectWithTag("Kick").GetComponent<FollowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Left and Right movement
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            isMoving = true;
            anim.SetBool("isMoving", isMoving);   
            playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);

            //I do it like this so that the player can knock back enemies the same direction they are facing
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isFacingLeft = false;
            }
            else if (horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isFacingLeft = true;
            }
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }

        //jumping code. it sets the players velocity to zero before adding force so that you can't use the velocity from your previous jump to rocket yourself up a wall
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("isJumping", isJumping);
        }

        //Melee attacking code
        if (Input.GetKeyDown(meleeAttackKey) && !isAttacking)
        {
            attackHitBox.enabled = true;
            attackSprite.enabled = true;
            isAttacking = true;
            anim.SetBool("isAttacking", isAttacking);
            StartCoroutine(Kicking());
        }

        if (!isFacingLeft)
        {
            kickFollowPlayerScript.thingToFollow = rightAttackPosition;
        }
        else
        {
            kickFollowPlayerScript.thingToFollow = leftAttackPosition;
        }

        if (Input.GetKeyDown(rangedAttackKey) && canShoot && shootingUnlocked)
        {
            Instantiate(bullet, placeToSpawnBullets.transform.position, placeToSpawnBullets.transform.rotation);
            canShoot = false;
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Kicking()
    {
        yield return new WaitForSeconds(kickTime);
        attackHitBox.enabled = false;
        attackSprite.enabled = false;
        yield return new WaitForSeconds(kickCoolDown);
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player can jump again when they touch the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            anim.SetBool("isJumping", isJumping);
        }

        if (collision.gameObject.CompareTag("UnlockShooting"))
        {
            shootingUnlocked = true;
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            gm.currentHealth -= enemyMeleeDamage;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            Destroy(collision.gameObject);

            gm.currentHealth -= enemyRangeDamage;
        }
    }
}
