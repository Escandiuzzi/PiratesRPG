using UnityEngine;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private GameObject _canvas;

    void Start()
    {
        //_button = gameObject.GetComponentInChildren<Button>();
        _canvas = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _canvas.GetComponent<Canvas>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canvas.GetComponent<Canvas>().enabled = false;
    }
}
