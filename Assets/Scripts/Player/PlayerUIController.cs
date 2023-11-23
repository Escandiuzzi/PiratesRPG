using System.Text;
using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] _crewSlotButtons;

    [SerializeField] private GameObject _hud;

    [SerializeField] private GameObject _slotPrefab;

    [SerializeField] private GameObject _crewPanel;

    [SerializeField] private GameObject _infoPanel;

    [SerializeField] private PlayerManagementController _playerManagement;

    [SerializeField] private GameObject _inventoryPanel;

    [SerializeField] private TextMeshProUGUI _inventoryText;

    [SerializeField] private bool _isCrewPanelOpen;

    private StringBuilder _sb;

    private void Start()
    {
        _sb = new StringBuilder();

        _crewSlotButtons = new GameObject[8];
        FindSlotButtonsAtPanel();

        _hud = GameObject.Find("Hud");
        _crewPanel = GameObject.Find("CrewSlotPanel");
        _infoPanel = GameObject.Find("InfoPanel");
        _inventoryPanel = GameObject.Find("InventoryPanel");
        _inventoryText = _inventoryPanel.transform.Find("Panel/Text").GetComponent<TextMeshProUGUI>();

        _playerManagement = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();

        _crewPanel.SetActive(false);
        _infoPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            CrewPanelInteraction();
    }

    private void FindSlotButtonsAtPanel()
    {
        for (var i = 0; i < _crewSlotButtons.Length; i++)
        {
            _crewSlotButtons[i] = GameObject.Find($"Slot {i + 1}");
            _crewSlotButtons[i].SetActive(false);
        }
    }

    private void CrewPanelInteraction()
    {
        UpdateInventoryInfo();

        _crewPanel.SetActive(!_crewPanel.activeSelf);
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);

        if (_isCrewPanelOpen)
        {
            _crewPanel.SetActive(false);
            _infoPanel.SetActive(false);
            _isCrewPanelOpen = false;
            HideSlotButtons();
            return;
        }

        _crewPanel.SetActive(true);
        _isCrewPanelOpen = true;

        var crew = _playerManagement.GetCrew();

        foreach (var pirate in crew)
        {
            var index = crew.IndexOf(pirate);

            var button = _crewSlotButtons[index];

            button.GetComponentInChildren<TextMeshProUGUI>().text = pirate.Name;
            button.GetComponent<CrewSlotButtonHandler>().PirateId = pirate.Id;
            button.SetActive(true);
        }
    }

    private void UpdateInventoryInfo()
    {
        _sb.Clear();

        var inventory = _playerManagement.GetInventory();

        foreach (var item in inventory) _sb.Append($"{item.Key.Name} {item.Value}x \n");

        _inventoryText.text = _sb.ToString();
    }

    private void HideSlotButtons()
    {
        foreach (var slot in _crewSlotButtons) slot.SetActive(false);
    }
}