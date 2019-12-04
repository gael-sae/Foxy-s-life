using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnimals : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)] float floatingDistance = 1;
    [SerializeField]
    int value = 1;

    Vector3 originalPosition;

    [SerializeField] AnimationCurve animationCurve_ = default;

    bool facingRight;
    float PosX;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float offsetX = animationCurve_.Evaluate(Time.time * 0.25f);
        transform.position = new Vector3(originalPosition.x + offsetX * floatingDistance, originalPosition.y);
        Flip(offsetX, PosX);
        PosX = offsetX;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControler>().AddMoney(value);
            Destroy(gameObject);
        }
    }
}