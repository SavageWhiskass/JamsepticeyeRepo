using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public PlayerStats playerStats;

    //private void OnMouseDown()
    //{
    //    healthManager.ReduceCurrentHealth(1);
    //}
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void TakeDamage(int amount)
    {
        playerStats.ReduceCurrentHealth(amount);
    }
}
