using UnityEngine;
using UnityEngine.EventSystems;

public class CrewSlotButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _infoPanel;

    public int PirateId { get; set; }

    void Start()
    {
        _infoPanel = GameObject.Find("InfoPanel");
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_infoPanel.activeSelf)
        {
            _infoPanel.SetActive(true);
            _infoPanel.GetComponent<PirateInfoPanelScript>().SetPirate(PirateId);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_infoPanel.activeSelf) _infoPanel.SetActive(false);
    }
}
