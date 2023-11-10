using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    public IslandType IslandType;

    public double IdleTime;

    public int Capacity;

    public GameObject InfoPanel;

    public GameObject PirateSelectionPanel;

    public GameObject[] _slots;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private PlayerManagementController _playerManagementController;

    [SerializeField]
    private List<Pirate> _piratesOnIsland;

    private void Start()
    {
        _playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();
        _piratesOnIsland = new List<Pirate>();
    }

    public void OnJoinButtonClicked()
    {
        InfoPanel.SetActive(false);
        PirateSelectionPanel.SetActive(true);

        SetPirateSelectionPanel();
    }

    private void SetPirateSelectionPanel()
    {
        HideSlots();

        var crew = _playerManagementController.GetAvailablePirates();

        for (int i = 0; i < crew.Count; i++)
        {
            var pirate = crew[i];

            var slot = _slots[i];
            slot.SetActive(true);
            slot.GetComponent<SlotToggleScript>().SetPirate(pirate);
        }
    }

    public void OnStartButtonPressed()
    {
        foreach (var slot in _slots)
        {
            if (slot.GetComponent<Toggle>().isOn)
            {
                var pirate = slot.GetComponent<SlotToggleScript>().GetPirate();
                pirate.IsBusy = true;

                _piratesOnIsland.Add(pirate);
            }
        }

        CloseAllPanels();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InfoPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseAllPanels();
    }

    private void CloseAllPanels()
    {
        InfoPanel.SetActive(false);
        PirateSelectionPanel.SetActive(false);
    }

    private void HideSlots()
    {
        foreach (var slot in _slots) slot.SetActive(false);
    }
}

public enum IslandType
{
    Mining = 0,
    Exploration = 1,
    Battle = 2
}
