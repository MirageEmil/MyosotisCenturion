using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOldHitBoxes : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", lifeTime);
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
