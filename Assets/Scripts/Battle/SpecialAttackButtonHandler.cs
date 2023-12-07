using Models;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpecialAttackButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject specialAttackInfoPanel;
    [FormerlySerializedAs("_specialAttackInfoPanelText")] [SerializeField] private TextMeshProUGUI specialAttackInfoPanelText;
        
    
    private Special _special;

    void Start()
    {
        specialAttackInfoPanel = GameObject.Find("Canvas").transform.Find("SpecialAttackInfo").gameObject;
        specialAttackInfoPanelText = specialAttackInfoPanel.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetSpecialAttack(Special special)
    {
        _special = special;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _special.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        specialAttackInfoPanel.SetActive(true);
        specialAttackInfoPanelText.text = UiTextFormatter.GetSpecialFormatted(_special);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        specialAttackInfoPanel.SetActive(false);
    }
}
