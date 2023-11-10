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

    public void SetPirate(int pirateId)
    {
        if (_playerManagementController == null)
            _playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();

        _pirate = _playerManagementController.GetPirateById(pirateId);
        UpdateUi();
    }

    private void UpdateUi()
    {
        if (_nameText == null) _nameText = GameObject.Find("Name");
        if (_statsText == null) _statsText = GameObject.Find("StatsText");

        _nameText.GetComponent<TextMeshProUGUI>().text = _pirate.Name;

        var textField = _statsText.GetComponent<TextMeshProUGUI>();

        var text = UiTextFormatter.GetPirateInfoAsText(_pirate);
        var pirateStatus = $"Status: {(_pirate.IsBusy == true ? "Busy" : "Available")}\n";

        textField.text = text + pirateStatus;
    }
}
