using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Vector3 _mousePos;

    [SerializeField]
    private float speed = 2f;

    float angle;

    private bool _moving;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestination();
            RotateToDirection();
        }

        if (_moving)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void SetDestination()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _moving = true;
    }

    private void RotateToDirection()
    {
        angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Move()
    {
        if (ArrivedAtDestination())
        {
            _moving = false;
            return;
        }

        RotateToDirection();

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _mousePos, step);
    }

    private bool ArrivedAtDestination()
    {
        return transform.position == _mousePos;
    }
}