using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask = default;
    Rigidbody2D body;
    BoxCollider2D boxCollider2d;

    [SerializeField]
    float speed = 3f;
    [SerializeField]
    float jumpVelocity = 6f;
    [SerializeField] int Bunny;

    bool facingRight;
    Vector2 direction;


    void Start()
    {
        facingRight = true;
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        body.velocity = direction;
        Flip(horizontal);
    }
    void Awake()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }
    bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if (IsGrounded() && Input.GetAxis("Jump") > 0.1f)
        {
            body.velocity = Vector2.up * jumpVelocity;
        }
    }
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    public void AddMoney(int value)
    {
        Bunny += value;
        Debug.Log("Money");
    }
}
