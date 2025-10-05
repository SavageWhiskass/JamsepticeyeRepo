using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject tripleJumpPrefab;
    [SerializeField] private GameObject bigBlastPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player.GetComponent<PlayerStats>().has_shield == false)
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
        else if (player.GetComponent<PlayerStats>().enemiesKilled > 10 && !player.GetComponent<PlayerStats>().triple_jump)
        {
            Instantiate(tripleJumpPrefab, transform.position, Quaternion.identity);
        }
        else if (player.GetComponent<PlayerStats>().enemiesKilled > 20 && !player.GetComponent<PlayerStats>().has_big_blast)
        {
            Debug.Log("huh");
            Instantiate(bigBlastPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
