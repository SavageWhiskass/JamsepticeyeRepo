using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.0f, 1.0f, -10.0f);
    [Range(0.0f, 1.0f)] public float smoothness = 0.5f; 
    
    Vector3 velocity; 

    private void Update ()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref velocity,
            smoothness); 
    }

    
}
