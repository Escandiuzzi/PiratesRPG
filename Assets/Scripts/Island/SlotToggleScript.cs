using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotToggleScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField]
    private bool _isSelected;

    [SerializeField]
    private Pirate _pirate;

    public GameObject TinyInfoPanel;

    public GameObject NameText;

    public GameObject StatsText;

    public void SetPirate(Pirate pirate)
    {
        _pirate = pirate;
    }

    public Pirate GetPirate() => _pirate;

    public void OnToggleValueChanged(Toggle toggle)
    {
        _isSelected = toggle.isOn;
    }

    private void AddPirateInfoToPanel()
    {
        NameText.GetComponent<TextMeshProUGUI>().text = _pirate.Name;
        StatsText.GetComponent<TextMeshProUGUI>().text = UiTextFormatter.GetPirateInfoAsText(_pirate);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TinyInfoPanel.SetActive(true);
        AddPirateInfoToPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TinyInfoPanel.SetActive(false);
    }
}
