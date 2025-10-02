using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public HealthManager healthManager;

    private void OnMouseDown()
    {
        healthManager.ReduceCurrentHealth(1);
    }
}
