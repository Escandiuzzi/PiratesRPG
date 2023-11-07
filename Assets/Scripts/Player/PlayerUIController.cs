using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _crewSlotButtons;

    [SerializeField]
    private GameObject _hud;

    [SerializeField]
    private GameObject _slotPrefab;

    [SerializeField]
    private GameObject _crewPanel;

    [SerializeField]
    private GameObject _infoPanel;

    [SerializeField]
    private PlayerManagementController _playerManagement;

    [SerializeField]
    private bool _isCrewPanelOpen;

    void Start()
    {
        _crewSlotButtons = new GameObject[8];
        FindSlotButtonsAtPanel();

        _hud = GameObject.Find("Hud");
        _crewPanel = GameObject.Find("CrewSlotPanel");
        _infoPanel = GameObject.Find("InfoPanel");

        _playerManagement = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();

        _crewPanel.SetActive(false);
        _infoPanel.SetActive(false);
    }

    private void FindSlotButtonsAtPanel()
    {
        for (int i = 0; i < _crewSlotButtons.Length; i++)
        {
            _crewSlotButtons[i] = GameObject.Find($"Slot {i + 1}");
            _crewSlotButtons[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            CrewPanelInteraction();
    }

    private void CrewPanelInteraction()
    {
        _crewPanel.SetActive(!_crewPanel.activeSelf);

        if (_isCrewPanelOpen)
        {
            _crewPanel.SetActive(false);
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

    private void HideSlotButtons() { foreach (var slot in _crewSlotButtons) slot.SetActive(false); }
}
