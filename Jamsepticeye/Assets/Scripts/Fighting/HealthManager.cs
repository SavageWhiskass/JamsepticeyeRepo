using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    int maxHealth = 3;
    int currentHealth = 3;
    GameObject[] hearts;
    GameObject player;
    Color activeHeart;
    Color hurtHeart;
    Color disabledHeart;

    public int SceneBuildIndex;

    private void Start()
    {
        hearts = new GameObject[100];
        GameObject[] heartCollection = GameObject.FindGameObjectsWithTag("Heart");
        foreach(GameObject heartUI in heartCollection)
        {
            hearts[heartUI.GetComponent<HeartIndex>().place - 1] = heartUI;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        activeHeart = Color.white;
        hurtHeart = new Color(0.114f, 0.114f, 0.114f, 0.184f);
        disabledHeart = new Color(0, 0, 0, 0);
    }

    public bool ReduceCurrentHealth(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            if(currentHealth >= 0)
            {
                hearts[currentHealth-1].GetComponent<Image>().color = hurtHeart;
                --currentHealth;
            }
        }

        return DeathCheck();
    }

    public void IncreaseCurrentHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (currentHealth < maxHealth)
            {
                hearts[currentHealth].GetComponent<Image>().color = activeHeart;
                ++currentHealth;
            }
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (maxHealth < 100)
            {
                hearts[maxHealth].GetComponent<Image>().color = hurtHeart;
                ++maxHealth;
                IncreaseCurrentHealth(1);
            }
        }
    }

    bool DeathCheck()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
