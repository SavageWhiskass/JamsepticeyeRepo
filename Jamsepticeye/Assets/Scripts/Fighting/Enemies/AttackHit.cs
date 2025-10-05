using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public PlayerStats stats;
    public int damage;
    public int knockback;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            stats.ReduceCurrentHealth(damage);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            stats.ReduceCurrentHealth(damage);
        }
    }
}
