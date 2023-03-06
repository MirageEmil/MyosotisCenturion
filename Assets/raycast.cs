using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public Transform playerDetector;
    // Start is called before the first frame update
    void Start()
    {
        playerDetector = gameObject.transform.Find("playerDetector").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerRay = Physics2D.Raycast(playerDetector.position, Vector2.down, 1f);
        if (playerRay.collider == null)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
