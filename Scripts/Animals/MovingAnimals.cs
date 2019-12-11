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

    float PosX;
    float speedCurve = 0.25f;

    enum State
    {
        PLAY,
        PAUSE
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float offsetX = animationCurve_.Evaluate(Time.time * speedCurve);
        transform.position = new Vector3(originalPosition.x + offsetX * floatingDistance, originalPosition.y);
        Flip(offsetX, PosX);
        PosX = offsetX;
    }
    
    void Flip(float offsetX, float oldX_)
    {
        if (offsetX > oldX_)
        {
            Vector3 Scale = transform.localScale;
            Scale.x = -1;
            transform.localScale = Scale;
        }
        else
        {
            Vector3 Scale = transform.localScale;
            Scale.x = 1;
            transform.localScale = Scale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControler>().AddBunny(value);
            Destroy(gameObject);
        }
    }
}