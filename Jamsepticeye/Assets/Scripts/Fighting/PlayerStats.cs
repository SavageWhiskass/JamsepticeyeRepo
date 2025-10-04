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
    [SerializeField] public int manaRegen = 1;
    float manaRegenCooldown = 1f;
    [SerializeField] private GameObject shield;
    bool shieldActive = false;
    [SerializeField] float shieldDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //shield = transform.Find("Shield")?.gameObject;
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            shield.SetActive(true);
            shieldActive = true;
            CancelInvoke(nameof(DisableShield));
            Invoke(nameof(DisableShield), shieldDuration);
        }
    }

    void DisableShield()
    {
        shield.SetActive(false);
        shieldActive = false;
    }

    private void FixedUpdate()
    {

    }


    public void ReduceCurrentHealth(int amount)
    {
        if (shieldActive)
        {
            return;
        }
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
            currentMana = maxMana;
        }
    }
}
