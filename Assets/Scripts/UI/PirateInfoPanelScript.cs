using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PirateInfoPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject nameText;

    [SerializeField]
    private GameObject statsText;

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
        if (nameText == null) nameText = gameObject.transform.Find("Panel/Name").gameObject;
        if (statsText == null) statsText = gameObject.transform.Find("Panel/StatsText").gameObject;

        nameText.GetComponent<TextMeshProUGUI>().text = _pirate.Name;

        var textField = statsText.GetComponent<TextMeshProUGUI>();

        var text = UiTextFormatter.GetPirateInfoAsText(_pirate);
        var pirateStatus = $"Status: {(_pirate.IsBusy ? "Busy" : "Available")}\n";

        textField.text = text + pirateStatus;
    }
}