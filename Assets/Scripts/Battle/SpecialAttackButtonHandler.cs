using Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialAttackButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject specialAttackInfoPanel;
    
    private Special _special;

    void Start()
    {
        specialAttackInfoPanel = GameObject.Find("Canvas").transform.Find("SpecialAttackInfo").gameObject;        
    }
    
    public void SetSpecialAttack(Special special)
    {
        _special = special;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _special.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        specialAttackInfoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        specialAttackInfoPanel.SetActive(false);
    }
}
