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
    public int enemiesKilled = 0;
    public int maxMana = 100;
    public int currentMana = 100;
    public int damage = 1;
    [SerializeField] public int manaRegen = 0;
    float manaRegenCooldown = 1f;
    public HealthManager healthManager;
    [SerializeField] private GameObject shield;
    bool shieldActive = false;
    [SerializeField] float shieldDuration = 2f;
    float shieldCooldown = 0f;

    public bool triple_jump = false;
    public bool has_shield = false;
    public bool has_big_blast = false;


    void Start()
    {

        shield.SetActive(false);
    }

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
        shieldCooldown = shieldCooldown - Time.deltaTime;
        if (has_shield && shieldCooldown <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shield.SetActive(true);
                shieldActive = true;
                CancelInvoke(nameof(DisableShield));
                Invoke(nameof(DisableShield), shieldDuration);
                shieldCooldown = shieldDuration + 3f;
            }
        }
    }

    void DisableShield()
    {
        shield.SetActive(false);
        shieldActive = false;
    }


    public void ReduceCurrentHealth(int amount)
    {
        if (shieldActive)
        {
            return;
        }
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
