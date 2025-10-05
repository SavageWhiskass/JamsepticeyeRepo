using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJumpUpgrade : MonoBehaviour
{
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            playerStats.triple_jump = true;
            Debug.Log("player now has triple jump");
            Destroy(gameObject);
        }

    }
}
