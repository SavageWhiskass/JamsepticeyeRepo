using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string levelName;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
