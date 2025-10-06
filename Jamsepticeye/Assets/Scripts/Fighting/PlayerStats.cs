using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 3;
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
    public static PlayerStats Instance;

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
        currentHealth = currentHealth - amount;
        if (shieldActive)
        {
            return;
        }
        DeathCheck(healthManager.ReduceCurrentHealth(amount));
    }

    public void IncreaseCurrentHealth(int amount)
    {
        currentHealth = currentHealth + amount;
        healthManager.IncreaseCurrentHealth(amount);
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth = maxHealth + amount;
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
            if(SceneBuildIndex == 1)
            {
                currentMana = maxMana;
                GetComponent<SpriteRenderer>().color = new Color(0.476f, 1, 0.984f, 0.509f);
                print("Switching scene to " + SceneBuildIndex);
                healthManager.IncreaseCurrentHealth(maxHealth);
                SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
                SceneBuildIndex = 4;
                //healthManager = FindObjectOfType<HealthManager>();
            }
            else if (SceneBuildIndex == 4)
            {
                currentMana = maxMana;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                print("Switching scene to " + SceneBuildIndex);
                healthManager.IncreaseCurrentHealth(maxHealth);
                SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
                SceneBuildIndex = 1;
                //healthManager = FindObjectOfType<HealthManager>();
            }
        }
    }
}
