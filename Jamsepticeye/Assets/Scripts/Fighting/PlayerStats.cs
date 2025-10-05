using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 3;
    int currentHealth = 3;
    public int SceneBuildIndex;
    int enemiesKilled = 0;
    public int maxMana = 100;
    public int currentMana = 100;
    public int damage = 1;
    [SerializeField] public int manaRegen = 0;
    float manaRegenCooldown = 1f;
    public HealthManager healthManager;


    // Update is called once per frame
    void Update()
    {
        manaRegenCooldown = manaRegenCooldown - Time.deltaTime;

        if (manaRegenCooldown <= 0f)
        {
            currentMana = currentMana + manaRegen;
            if(currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            manaRegenCooldown = 1f;
        }
    }


    public void ReduceCurrentHealth(int amount)
    {
        DeathCheck(healthManager.ReduceCurrentHealth(amount));
    }

    public void IncreaseCurrentHealth(int amount)
    {
        healthManager.IncreaseCurrentHealth(amount);
    }

    public void IncreaseMaxHealth(int amount)
    {
        healthManager.IncreaseMaxHealth(amount);
    }

    public void RegisterKill()
    {
        enemiesKilled++;
        Debug.Log(enemiesKilled);
    }

    void DeathCheck(bool isDead)
    {
        if (isDead)
        {
            print("Switching scene to " + SceneBuildIndex);
            SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
            currentHealth = maxHealth;
            currentMana = maxMana;
        }
    }
}
