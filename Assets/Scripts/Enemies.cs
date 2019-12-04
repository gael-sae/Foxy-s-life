using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
   // [SerializeField] [Range(5, 0)] float floatingDistance = 1;

    Vector3 originalPosition;

    [SerializeField]
    Vector3 leftOffset = default;
    [SerializeField]
    Vector3 rightOffset = default;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float speedChase = 2;

    Vector3 leftTarget;
    Vector3 rightTarget;

    enum State
    {
        IDLE,
        PATROLLE,
        CHASE_PLAYER
    }

    State state = State.IDLE;
    bool isGoingRight = true;
    Rigidbody2D enemie;
    Transform targetChase;

    bool facingRight;
    float PosX;

    void Start()
    {
        leftTarget = transform.position + leftOffset;
        rightTarget = transform.position + rightOffset;
        enemie = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                state = State.PATROLLE;
                break;
            case State.PATROLLE:
                if (isGoingRight)
                {
                    Vector3 velocity = (rightTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, 0);

                    enemie.velocity = velocity;
                    if (Vector3.Distance(transform.position, rightTarget) < 0.1f)
                    {
                        isGoingRight = false;
                    }
                }
                else
                {
                    Vector3 velocity = (leftTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, 0);

                    enemie.velocity = velocity;

                    if (Vector3.Distance(transform.position, leftTarget) < 0.1f)
                    {
                        isGoingRight = true;
                    }
                }
                break;
            case State.CHASE_PLAYER:
                {
                    Vector3 velocity = (targetChase.position - transform.position).normalized * speedChase;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, 0);

                    if (transform.position.x + velocity.x * Time.deltaTime >= rightTarget.x || transform.position.x + velocity.x * Time.deltaTime <= leftTarget.x)
                    {
                        enemie.velocity = new Vector2(0, 0);
                    }
                    else
                    {
                        enemie.velocity = velocity;
                    }
                }
                break;
        }
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
            state = State.CHASE_PLAYER;
            targetChase = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = State.PATROLLE;
        }
    }

    void OnDrawGizmos()
    {
        //Leff point
        if (leftTarget == Vector3.zero)
        {
            Gizmos.DrawWireCube(transform.position + leftOffset, Vector3.one);
        }
        else
        {
            Gizmos.DrawWireCube(leftTarget, Vector3.one);
        }

        //right point
        if (rightTarget == Vector3.zero)
        {
            Gizmos.DrawWireCube(transform.position + rightOffset, Vector3.one);
        }
        else
        {
            Gizmos.DrawWireCube(rightTarget, Vector3.one);
        }
    }
}

