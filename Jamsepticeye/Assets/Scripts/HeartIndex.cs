using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartIndex : MonoBehaviour
{
    public int place;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
