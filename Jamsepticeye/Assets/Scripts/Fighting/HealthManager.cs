using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 3;
    [SerializeField] GameObject[] hearts;
    [SerializeField] GameObject[] hearts2;
    GameObject player;
    Color activeHeart;
    Color hurtHeart;
    Color disabledHeart;

    public int SceneBuildIndex;
    public static HealthManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        hearts = new GameObject[100];
        hearts2 = new GameObject[100];
        GameObject[] heartCollection = GameObject.FindGameObjectsWithTag("Heart");
        foreach(GameObject heartUI in heartCollection)
        {
            hearts[heartUI.GetComponent<HeartIndex>().place - 1] = heartUI;
            hearts2[heartUI.GetComponent<HeartIndex>().place - 1] = heartUI;
        }
        activeHeart = Color.white;
        hurtHeart = new Color(0.114f, 0.114f, 0.114f, 0.184f);
        disabledHeart = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //maxHealth = player.GetComponent<PlayerStats>().maxHealth;
        //currentHealth = player.GetComponent<PlayerStats>().currentHealth;
        //hearts = new GameObject[100];
        //GameObject[] heartCollection = GameObject.FindGameObjectsWithTag("Heart");
        //foreach (GameObject heartUI in heartCollection)
        //{
        //    hearts[heartUI.GetComponent<HeartIndex>().place - 1] = heartUI;
        //}
        //activeHeart = Color.white;
        //hurtHeart = new Color(0.114f, 0.114f, 0.114f, 0.184f);
        //disabledHeart = new Color(0, 0, 0, 0);
    }

    public bool ReduceCurrentHealth(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            if(currentHealth >= 1)
            {
                Debug.Log(hearts.Length);
                Debug.Log(currentHealth - 1);
                Debug.Log(hearts[currentHealth - 1]);
                Debug.Log(hearts[currentHealth - 1].GetComponent<Image>());
                Debug.Log(hearts[currentHealth - 1].GetComponent<Image>().color);
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
