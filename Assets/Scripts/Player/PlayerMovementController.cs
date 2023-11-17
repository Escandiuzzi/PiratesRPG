using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
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
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement(GetEventSystemRaycastResults()))
        {
            SetDestination();
            RotateToDirection();
        }

        if (_moving)
        {
            Move();
        }
    }

    /// Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];

            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }

        return false;
    }

    /// Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raysastResults = new();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        return raysastResults;
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
