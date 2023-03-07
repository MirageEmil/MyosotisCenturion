using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject thingToFollow;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = thingToFollow.transform.position + offset;
    }
}
