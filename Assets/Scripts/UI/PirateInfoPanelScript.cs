using System.Text;
using TMPro;
using UnityEngine;

public class PirateInfoPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _nameText;

    [SerializeField]
    private GameObject _statsText;

    [SerializeField]
    private Pirate _pirate;

    private PlayerManagementController _playerManagementController;

    void Start()
    {
        _nameText = GameObject.Find("Name");
        _statsText = GameObject.Find("StatsText");

        _playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();
    }

    public void SetPirate(int pirateId)
    {
        _pirate = _playerManagementController.GetPirateById(pirateId);
        UpdateUi();
    }

    private void UpdateUi()
    {
        _nameText.GetComponent<TextMeshProUGUI>().text = _pirate.Name;

        var textField = _statsText.GetComponent<TextMeshProUGUI>();
        textField.text = "";

        var statsText = new StringBuilder();

        statsText.Append($"HP: {_pirate.Hp}/{_pirate.MaxHp}\n");
        statsText.Append($"Energy: {_pirate.Energy}/{_pirate.MaxEnergy}\n");
        statsText.Append($"Attack: {_pirate.AttackingPoints}\n");
        statsText.Append($"Mining: {_pirate.MiningPoints}\n");
        statsText.Append($"Cooking: {_pirate.CookingPoints}\n");

        textField.text = statsText.ToString();
    }
}
