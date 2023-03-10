using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float speed = 2;
    public Transform groundDetector;
    public Transform playerDetector;
    // Start is called before the first frame update
    void Start()
    {
        playerDetector = gameObject.transform.Find("playerDetector").transform;
        groundDetector = gameObject.transform.Find("groundDetector").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerRay = Physics2D.Raycast(playerDetector.position, Vector2.right, 1f);

        if (playerRay.collider!=null)
        {

            if (playerRay.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("i am touching: " + playerRay.collider.gameObject.name);
            }
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            RaycastHit2D groundRay = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f);
            if (groundRay.collider == null)
            {
                transform.Rotate(0f, 180f, 0f);
            }

        }
    }
}
