using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int currentHealth = 3;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHealth;

    public Transform Spawnpoint;

    bool Death = false;

    [Header("sounds")]
    [SerializeField] List<SO_Clip> hurtClips_;
    AudioManager audioManager_;

    enum State
    {
        PLAY,
        GAMEOVER
    }
    State state = State.PLAY;

    void Start()
    {
        currentHealth = maxHealth;
        audioManager_ = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        switch (state)
        {
            case State.PLAY:
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                for (int i = 0; i < heart.Length; i++)
                {
                    if (i < currentHealth)
                    {
                        heart[i].sprite = fullHeart;
                    }
                    else
                    {
                        heart[i].sprite = emptyHealth;
                    }
                    if (i < maxHealth)
                    {
                        heart[i].enabled = true;
                    }
                    else
                    {
                        heart[i].enabled = false;
                    }
                }
                if (currentHealth <= 0)
                {
                    state = State.GAMEOVER;
                }
                break;
            case State.GAMEOVER:
                Destroy(gameObject);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "OutMap")
        {
            currentHealth--;
            Death = true;
            audioManager_.PlayWithRandomPitch(hurtClips_[Random.Range(0, hurtClips_.Count)]);
        }
        if (other.gameObject.tag == "Enemies")
        {
            currentHealth--;
            Death = true;
            audioManager_.PlayWithRandomPitch(hurtClips_[Random.Range(0, hurtClips_.Count)]);
        }
        if (currentHealth > 0 && Death)
        {
            Respawn();
            Death = false;
        }
    }

    void Respawn()
    {
        this.transform.position = Spawnpoint.position;
    }
}





