using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CrewSlotButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject infoPanel;

    public int PirateId { get; set; }

    private void Start()
    {
        infoPanel = GameObject.Find("InfoPanel");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (infoPanel.activeSelf) return;
        
        infoPanel.SetActive(true);
        infoPanel.GetComponent<PirateInfoPanelScript>().SetPirate(PirateId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (infoPanel.activeSelf) infoPanel.SetActive(false);
    }
}