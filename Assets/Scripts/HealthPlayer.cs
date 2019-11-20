using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{

    [SerializeField]
    int maxHealth = 3;

    int currentHealth;

    public Transform Spawnpoint;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OutMap")
        {
            Respawn();
            currentHealth--;
            Debug.Log("ici");
        }       
        if (other.gameObject.tag == "Enemies")
        {
            Respawn();
            currentHealth--;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Respawn()
    {
        this.transform.position = Spawnpoint.position;
    }
}





