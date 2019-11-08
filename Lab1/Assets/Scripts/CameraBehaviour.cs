using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour 
{
    public Transform target;

    private Vector3 _offset;

    public float Speed = 1;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    public void Update()
    {
        var targetPosition = target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Speed * Time.deltaTime);
    }
}
