using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;

    [SerializeField]
    private Vector3 mousePos;

    [SerializeField] 
    private float speed = 2f;

    [SerializeField] 
    private bool moving;
    
    [SerializeField] 
    private float angle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement(GetEventSystemRaycastResults()))
        {
            SetDestination();
            RotateToDirection();
        }

        if (moving) Move();
    }

    /// Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(IEnumerable<RaycastResult> eventSystemRaycastResults) =>
        eventSystemRaycastResults.Any(systemRaycastResult => systemRaycastResult.gameObject.layer == LayerMask.NameToLayer("UI"));
    

    /// Gets all event system raycast results of current mouse or touch position.
    private static IEnumerable<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        return raycastResults;
    }

    private void SetDestination()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moving = true;
    }

    private void RotateToDirection()
    {
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Move()
    {
        if (ArrivedAtDestination())
        {
            moving = false;
            return;
        }

        RotateToDirection();

        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, mousePos, step);
    }

    private bool ArrivedAtDestination()
    {
        return transform.position == mousePos;
    }

    public void EnteredInIslandRange()
    {
        moving = false;
    }
}