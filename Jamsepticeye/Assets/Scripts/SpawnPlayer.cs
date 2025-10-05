using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPLayer : MonoBehaviour
{
    [SerializeField] public int SceneBuildIndex;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindWithTag("Player")?.transform;
        var scene = SceneManager.GetSceneByBuildIndex(SceneBuildIndex);
        var spawnPoint = GameObject.FindWithTag("SpawnPoint");
        Vector3 spawnPos = spawnPoint.transform.position;
        spawnPos.z = player.position.z;      // was: transform.position.z
        player.position = spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
