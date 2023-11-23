using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    private readonly float _dampTime = 0.15f;
    private Transform _target;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_target)
        {
            var point = Camera.main.WorldToViewportPoint(_target.position);
            var delta = _target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            var destination = transform.position + delta;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime);
        }
    }
}