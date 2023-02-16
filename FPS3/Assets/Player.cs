using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHelath = 100;
    public int currentHealth;
    public Text healthPoints;
    public HealcthBarScript healcthBar;
    public Target target;
    void Start()
    {
        currentHealth = maxHelath;
        healcthBar.SetMaxHealth(maxHelath);
        healthPoints.text = currentHealth + "/" + maxHelath;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            currentHealth -= 10;
            healcthBar.SetHealth(currentHealth);
            healthPoints.text = currentHealth + "/" + maxHelath;

        }
    }
}
