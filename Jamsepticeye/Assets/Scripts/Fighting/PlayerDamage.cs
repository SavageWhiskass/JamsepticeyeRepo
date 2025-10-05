using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void TakeDamage(int amount)
    {
        playerStats.ReduceCurrentHealth(amount);
    }

    public void StunPlayerTrigger(float duration)
    {
        StartCoroutine(StunPlayer(duration));
    }

    IEnumerator StunPlayer(float duration)
    {
        var Movement = GetComponent<MovementScript>();
        Movement.enabled = false;
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        Movement.enabled = true;
    }
}
