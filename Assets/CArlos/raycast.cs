using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float moveSpeed;
    public float shootCoolDownTime = 2.5f;
    public Transform groundDetector;
    public Transform playerDetector;
    public GameObject EnemyProjectile;
    public GameObject spotToLaunchProjectile;
    public bool isALaserEnemy = false;
    private bool isRotated = false;
    public bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        playerDetector = gameObject.transform.Find("playerDetector").transform;
        groundDetector = gameObject.transform.Find("groundDetector").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (canShoot && isALaserEnemy)
        {
            Instantiate(EnemyProjectile, spotToLaunchProjectile.transform.position, spotToLaunchProjectile.transform.rotation);
            canShoot = false;
            StartCoroutine(laserCoolDown());
        } 

        RaycastHit2D groundDetection = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f);
        if (!isRotated)
        {
            RaycastHit2D wallDetection = Physics2D.Raycast(playerDetector.position, Vector2.right, 0.1f);
            if (wallDetection.collider != null && wallDetection.collider.gameObject.CompareTag("Ground") || groundDetection.collider == null)
            {
                isRotated = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            RaycastHit2D wallDetection = Physics2D.Raycast(playerDetector.position, Vector2.left, 0.1f);
            if (wallDetection.collider != null && wallDetection.collider.gameObject.CompareTag("Ground") || groundDetection.collider == null)
            {
                isRotated = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }

          RaycastHit2D PlayerDetection = Physics2D.Raycast(playerDetector.position, Vector2.left, 3f);
          if (PlayerDetection.collider != null && PlayerDetection.collider.gameObject.CompareTag("Player") && canShoot && isALaserEnemy)
          {
              Debug.Log("shoot the player");
              Instantiate(EnemyProjectile, spotToLaunchProjectile.transform.position, spotToLaunchProjectile.transform.rotation);
              canShoot = false;
              StartCoroutine(laserCoolDown());
          }
        

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canShoot && isALaserEnemy)
        {
            Debug.Log("shoot bullet");
            Instantiate(EnemyProjectile, spotToLaunchProjectile.transform.position, spotToLaunchProjectile.transform.rotation);
            canShoot = false;
            StartCoroutine(laserCoolDown());
        } 
    }

    IEnumerator laserCoolDown()
    {
        yield return new WaitForSeconds(shootCoolDownTime);
        canShoot = true;
    }
}
