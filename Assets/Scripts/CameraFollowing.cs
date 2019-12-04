using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    float zValue = -10;

    [SerializeField] Transform target_ = default;

    [SerializeField] bool doLerp = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (doLerp)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target_.position.x, 0, zValue), 0f);           
        }
        if (target_ == null)
        {
            transform.position = new Vector3(0, 0, zValue);
        }
        else
        {
            transform.position = target_.position;

            transform.position = new Vector3(transform.position.x, 0, zValue);
        }
    }
}

