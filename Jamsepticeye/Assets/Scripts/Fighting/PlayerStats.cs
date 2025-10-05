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
<<<<<<< Updated upstream
    public HealthManager healthManager;

=======
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    public void ReduceCurrentHealth(int amount)
    {
        DeathCheck(healthManager.ReduceCurrentHealth(amount));
    }

    public void IncreaseCurrentHealth(int amount)
    {
        healthManager.IncreaseCurrentHealth(amount);
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
            animator.SetTrigger("die");
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
}
