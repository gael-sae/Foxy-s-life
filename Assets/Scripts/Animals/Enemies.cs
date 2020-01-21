using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
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
    public bool Idle = true;
    float PosX;

    Animator animator_;


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

    int ignorePos = 0;
    float marge = 0.1f;

    void Start()
    {
        leftTarget = transform.position + leftOffset;
        rightTarget = transform.position + rightOffset;
        enemie = GetComponent<Rigidbody2D>();
        animator_ = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                state = State.PATROLLE;
                
                         break;
            case State.PATROLLE:
                UpdateAnimation();
                if (isGoingRight)
                {
                    Vector3 velocity = (rightTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, ignorePos);

                    enemie.velocity = velocity;
                    if (Vector3.Distance(transform.position, rightTarget) < marge)
                    {
                        isGoingRight = false;
                    }
                    Flip(velocity.x, PosX);
                    PosX = velocity.x;
                }
                else
                {
                    Vector3 velocity = (leftTarget - transform.position).normalized * speed;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, ignorePos);

                    enemie.velocity = velocity;

                    if (Vector3.Distance(transform.position, leftTarget) < marge)
                    {
                        isGoingRight = true;
                    }
                    Flip(velocity.x, PosX);
                    PosX = velocity.x;
                }
                break;
            case State.CHASE_PLAYER:
                {
                    UpdateAnimation();
                    Vector3 velocity = (targetChase.position - transform.position).normalized * speedChase;
                    velocity = new Vector3(velocity.x, enemie.velocity.y, ignorePos);

                    if (transform.position.x + velocity.x * Time.deltaTime >= rightTarget.x || transform.position.x + velocity.x * Time.deltaTime <= leftTarget.x)
                    {
                        enemie.velocity = new Vector2(ignorePos, ignorePos);
                    }
                    else
                    {
                        enemie.velocity = velocity;
                    }
                    Flip(velocity.x, PosX);
                    PosX = velocity.x;
                }
                break;
        }
    }
    void UpdateAnimation()
    {
        animator_.SetFloat("speedChase", Mathf.Abs(enemie.velocity.x));
    }

    void Flip(float velocity, float oldX_)
    {
        if (velocity > oldX_)
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
        if (leftTarget == Vector3.zero)
        {
            Gizmos.DrawWireCube(transform.position + leftOffset, Vector3.one);
        }
        else
        {
            Gizmos.DrawWireCube(leftTarget, Vector3.one);
        }

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

