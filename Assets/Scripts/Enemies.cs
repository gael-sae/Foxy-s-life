using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] [Range(5, 0)] float floatingDistance = 1;

    Vector3 originalPosition;

    [SerializeField]
    AnimationCurve animationCurve_;
    
    bool facingRight;
    float PosX;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float offsetX = animationCurve_.Evaluate(Time.time);
        transform.position = new Vector3(originalPosition.x, originalPosition.y + offsetX * floatingDistance);
    }
    void Flip(float offsetX, float oldX_)
    {
        if (offsetX > oldX_)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
    }
    void OnDrawGizmos()
    {
        if (originalPosition == Vector3.zero)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + floatingDistance));
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - floatingDistance));
        }
        else
        {
            Gizmos.DrawLine(originalPosition, new Vector2(originalPosition.x, originalPosition.y + floatingDistance));
            Gizmos.DrawLine(originalPosition, new Vector2(originalPosition.x, originalPosition.y - floatingDistance));
        }
    }
}
