using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SlotToggleScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject island;
    
    [SerializeField]
    private GameObject tinyInfoPanel;

    [SerializeField]
    private GameObject nameText;

    [SerializeField]
    private GameObject statsText;

    private IslandController _islandController;
    
    private Pirate _pirate;

    public void Start()
    {
        island = gameObject.transform.parent.parent.parent.parent.gameObject;
        tinyInfoPanel = island.transform.Find("Canvas/TinyInfoPanel").gameObject;
        nameText = tinyInfoPanel.transform.Find("Name").gameObject;
        statsText = tinyInfoPanel.transform.Find("Stats").gameObject;

        _islandController = island.GetComponent<IslandController>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        tinyInfoPanel.SetActive(true);
        AddPirateInfoToPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tinyInfoPanel.SetActive(false);
    }

    public void SetPirate(Pirate pirate)
    {
        _pirate = pirate;
    }

    public Pirate GetPirate()
    {
        return _pirate;
    }

    public void OnToggleValueChanged(Toggle toggle)
    {
        toggle.isOn = _islandController.TryAddPirate();
    }

    private void AddPirateInfoToPanel()
    {
        nameText.GetComponent<TextMeshProUGUI>().text = _pirate.Name;
        statsText.GetComponent<TextMeshProUGUI>().text = UiTextFormatter.GetPirateInfoAsText(_pirate);
    }
}