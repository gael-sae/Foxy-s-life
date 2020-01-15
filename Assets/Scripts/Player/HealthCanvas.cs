using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{

    public int currentHealth;
    public int numOfHealths = 3;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHealth;

    HealthPlayer healthPlayer;
    PlayerControler playerControler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > numOfHealths)
        {
            currentHealth = numOfHealths;
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
            if (i < numOfHealths)
             {
                heart[i].enabled = true;
             }
             else
            {
                heart[i].enabled = false;
            }
            
        }
    }
}
