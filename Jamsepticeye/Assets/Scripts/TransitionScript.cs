using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{

    public int SceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        if (other.tag == "Player")
        {
            var player = GameObject.FindWithTag("Player");
            var stats = player.GetComponent<PlayerStats>();
            stats.currentMana = stats.maxMana;
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            print("Switching scene to " + SceneBuildIndex);
            HealthManager healthManager = FindObjectOfType<HealthManager>();
            healthManager.IncreaseCurrentHealth(stats.maxHealth);
            SceneBuildIndex = 1;
            SceneManager.LoadScene(SceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
