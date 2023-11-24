using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] crewSlotButtons;

    [SerializeField] private GameObject hud;

    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private GameObject crewPanel;

    [SerializeField] private GameObject infoPanel;

    [SerializeField] private PlayerManagementController playerManagement;

    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private TextMeshProUGUI inventoryText;

    [SerializeField] private bool isCrewPanelOpen;

    private StringBuilder _sb;

    private void Start()
    {
        _sb = new StringBuilder();

        crewSlotButtons = new GameObject[8];
        FindSlotButtonsAtPanel();

        hud = GameObject.Find("Hud");
        crewPanel = GameObject.Find("CrewSlotPanel");
        infoPanel = GameObject.Find("InfoPanel");
        inventoryPanel = GameObject.Find("InventoryPanel");
        inventoryText = inventoryPanel.transform.Find("Panel/Text").GetComponent<TextMeshProUGUI>();

        playerManagement = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();

        crewPanel.SetActive(false);
        infoPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            CrewPanelInteraction();
    }

    private void FindSlotButtonsAtPanel()
    {
        for (var i = 0; i < crewSlotButtons.Length; i++)
        {
            crewSlotButtons[i] = GameObject.Find($"Slot {i + 1}");
            crewSlotButtons[i].SetActive(false);
        }
    }

    private void CrewPanelInteraction()
    {
        UpdateInventoryInfo();

        crewPanel.SetActive(!crewPanel.activeSelf);
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        if (isCrewPanelOpen)
        {
            crewPanel.SetActive(false);
            infoPanel.SetActive(false);
            isCrewPanelOpen = false;
            HideSlotButtons();
            return;
        }

        crewPanel.SetActive(true);
        isCrewPanelOpen = true;

        var crew = playerManagement.GetCrew();

        foreach (var pirate in crew)
        {
            var index = crew.IndexOf(pirate);

            var button = crewSlotButtons[index];

            button.GetComponentInChildren<TextMeshProUGUI>().text = pirate.Name;
            button.GetComponent<CrewSlotButtonHandler>().PirateId = pirate.Id;
            button.SetActive(true);
        }
    }

    private void UpdateInventoryInfo()
    {
        _sb.Clear();

        var inventory = playerManagement.GetInventory();

        foreach (var item in inventory) _sb.Append($"{item.Key.Name} {item.Value}x \n");

        inventoryText.text = _sb.ToString();
    }

    private void HideSlotButtons()
    {
        foreach (var slot in crewSlotButtons) slot.SetActive(false);
    }
}