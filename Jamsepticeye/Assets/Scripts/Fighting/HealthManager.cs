using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    int maxHealth = 3;
    int currentHealth = 3;
    public GameObject[] hearts;
    public GameObject player;

    public void ReduceCurrentHealth(int amount)
    {
        if ((currentHealth - amount) < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= amount;
        }

        UpdateGUI();
        DeathCheck();
    }

    public void IncreaseCurrentHealth(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        UpdateGUI();
        DeathCheck();
    }

    void UpdateGUI()
    {
        switch (currentHealth)
        {
            case 0:
                hearts[2].SetActive(false);
                hearts[1].SetActive(false);
                hearts[0].SetActive(false);
                break;
            case 1:
                hearts[2].SetActive(false);
                hearts[1].SetActive(false);
                hearts[0].SetActive(true);
                break;
            case 2:
                hearts[2].SetActive(false);
                hearts[1].SetActive(true);
                hearts[0].SetActive(true);
                break;
            case 3:
                hearts[2].SetActive(true);
                hearts[1].SetActive(true);
                hearts[0].SetActive(true);
                break;
        }
    }

    void DeathCheck()
    {
        if(currentHealth == 0)
        {
            player.GetComponent<Rigidbody2D>().freezeRotation = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //Play death animation and open Game Over screen
        }
    }
}
