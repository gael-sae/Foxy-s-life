using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D body;
    BoxCollider2D boxCollider2d;
    [SerializeField] GameObject panelVictory;

    [SerializeField]
    float speed = 2f;
    float runStop = 0f;
    [SerializeField]
    float jumpVelocity = 4f;
    [SerializeField]
    int Bunny;
    [SerializeField]
    int BunnyMinim = 3;

    bool facingRight;
    Vector2 direction;

    bool Win = false;
    bool Idle = false;

    [SerializeField]int Platform = 0;
    float DestroyTime = 0.4f;
    float Marge = 0.1f;
    int layerPlayer = 10;
    int layerWinObject = 9;

    enum State
    {
        PLAY,
        PAUSE,
        WINGAME,
    }
    State state = State.PLAY;

    void Start()
    {
        facingRight = true;
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        panelVictory.SetActive(false);
    }

    void FixedUpdate()
    {
        body.velocity = direction;
    }
  
    void Update()
    {
        switch (state)
        {
            case State.PLAY:

                float horizontal = Input.GetAxis("Horizontal");
                Flip(horizontal);
                direction = new Vector2(horizontal * speed, body.velocity.y);

                if (Platform > 0 && Input.GetAxis("Jump") > Marge && !Win && Mathf.Abs(body.velocity.y) <= Marge)
                {
                    direction = Vector2.up * jumpVelocity;
                }

                if (Input.GetKeyDown(KeyCode.P))
                {
                    Idle = true;
                    state = State.PAUSE;
                }
                break;
            case State.PAUSE:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Idle = false;
                        state = State.PLAY;
                    }
                break;
            case State.WINGAME:
                        direction = new Vector2(-1, 0) * speed;
                        panelVictory.SetActive(true);
                    if (transform.position.x <= -5.7)
                    {
                    speed = runStop;
                    Destroy(gameObject, DestroyTime);
                    }
                break;
        }

       
    }
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight && !Win || horizontal < 0 && facingRight && !Win)
        {
            facingRight = !facingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }
    public void AddBunny(int value)
    {
        Bunny += value;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "WinObject" && Bunny >= BunnyMinim)
        {
            Win = true;
            state = State.WINGAME;
            Physics2D.IgnoreLayerCollision(layerPlayer, layerWinObject, Win);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Platform--; 
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform" )
        {
            Platform++;
        }
    }
    void WinGame()
    {
    }
}
