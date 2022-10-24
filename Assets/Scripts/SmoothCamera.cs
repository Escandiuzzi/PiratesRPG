using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    private Vector3 _velocity = Vector3.zero;

    private float _dampTime = 0.15f;
    private Transform _target;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(_target.position);
            Vector3 delta = _target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
        }

    }
}
