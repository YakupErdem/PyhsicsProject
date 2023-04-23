using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SmoothCameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0, -10f);
    public float smoothTime = .25f;
    public Vector3 velocity = Vector3.zero;

    public Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
