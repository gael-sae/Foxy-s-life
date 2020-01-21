using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D body;
    BoxCollider2D boxCollider2d;
    [SerializeField] 
    GameObject panelVictory;

    [SerializeField]
    float speed = 2f;
    float runStop = 0f;
    [SerializeField]
    float jumpVelocity = 4f;
    [SerializeField]
    int bunnyCatch = 0;
    [SerializeField]
    int BunnyMinim = 3;

    bool facingRight;
    Vector2 direction;

    bool Win = false;
    bool Idle = true;
    bool jumpUP = false;

    [SerializeField]
    int Platform = 0;

    float Marge = 0.1f;
    int layerPlayer = 10;
    int layerWinObject = 9;

    Animator animator_;

    public Image[] bunny;
    public Sprite fullBunny;
    public Sprite emptyBunny;
    [SerializeField]
    int maxBunny = 5;
    private SpriteRenderer sprite;
    int sortingOrder = 0;

    enum State
    {
        PLAY,
        WINGAME,
    }
    State state = State.PLAY;

    void Start()
    {
        facingRight = true;
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        panelVictory.SetActive(false);
        animator_ = GetComponentInChildren<Animator>();
        state = State.PLAY;
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        body.velocity = direction;
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1 & !Idle)
            {
                Time.timeScale = 0;
                Idle = true;
            }
            else
            {
                Time.timeScale = 1;
                Idle = false;
            }
        }



        for (int i = 0; i < bunny.Length; i++)
        {
            if (i < bunnyCatch)
            {
                bunny[i].sprite = fullBunny;
            }
            else
            {
                bunny[i].sprite = emptyBunny;
            }
            if (i < maxBunny)
            {
                bunny[i].enabled = true;
            }
            else
            {
                bunny[i].enabled = false;
            }
        }

        switch (state)
        {
            case State.PLAY:
                UpdateAnimation();
                float horizontal = Input.GetAxis("Horizontal");
                Flip(horizontal);
                direction = new Vector2(horizontal * speed, body.velocity.y);

                if (Platform > 0 && Input.GetAxis("Jump") > Marge && !Win && Mathf.Abs(body.velocity.y) <= Marge & !jumpUP)
                {
                    Debug.Log("button down");
                    direction = Vector2.up * jumpVelocity;
                    jumpUP = true;
                }
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    Debug.Log("button up");
                    jumpUP = false;
                }

                break;
            case State.WINGAME:
                        direction = new Vector2(-1, 0) * speed;
                        panelVictory.SetActive(true);
                    if (transform.position.x <= -5.7)
                    {
                    speed = runStop;
                    sprite.sortingOrder = sortingOrder;
                }
                break;
        }
    }
    void UpdateAnimation()
    {
        animator_.SetFloat("speed", Mathf.Abs(body.velocity.x));
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
        bunnyCatch += value;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "WinObject" && bunnyCatch >= BunnyMinim)
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
}
