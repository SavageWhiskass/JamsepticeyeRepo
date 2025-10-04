using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float startPos;
    public float effectStrength;

    private void Start()
    {
        startPos = transform.position.x;
    }

    private void FixedUpdate()
    {
        float dist = Camera.main.transform.position.x * effectStrength;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
