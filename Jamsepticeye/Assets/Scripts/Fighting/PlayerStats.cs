using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    int maxHealth = 3;
    int currentHealth = 3;
    public int SceneBuildIndex;
    int enemiesKilled = 0;
    public int maxMana = 100;
    public int currentMana = 100;
    public int manaRegen = 1;
    float manaRegenCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manaRegenCooldown = manaRegenCooldown - Time.deltaTime;

        if (manaRegenCooldown <= 0f)
        {
            currentMana = currentMana + manaRegen;
            manaRegenCooldown = 1f;
        }
    }

    private void FixedUpdate()
    {

    }


    public void ReduceCurrentHealth(int amount)
    {
        Debug.Log("ow!");
        Debug.Log(currentHealth);
        if ((currentHealth - amount) < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= amount;
        }
        DeathCheck();
    }

    public void IncreaseCurrentHealth(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        DeathCheck();
    }

    public void RegisterKill()
    {
        enemiesKilled++;
        Debug.Log(enemiesKilled);
    }

    void DeathCheck()
    {
        if (currentHealth == 0)
        {
            print("Switching scene to " + SceneBuildIndex);
            SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
            currentHealth = maxHealth;
        }
    }
}
