using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    public Region Region;

    public IslandType IslandType;

    public float IdleTime;

    public int Capacity;

    public int MaxRewardsNumber;

    public bool Renewable;

    public bool _hasActivityStarted;

    public GameObject InfoPanel;

    public GameObject PirateSelectionPanel;

    public GameObject RewardsPanel;

    public GameObject ProgressBar;

    public GameObject JoinButton;

    public GameObject CollectButton;

    public GameObject RewardsText;

    public GameObject InfoText;

    [SerializeField]
    private GameObject[] _slots;

    [SerializeField]
    private float _currentIdleTime;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Slider _progressBarSlider;

    [SerializeField]
    private PlayerManagementController _playerManagementController;

    [SerializeField]
    private List<Pirate> _piratesOnIsland;

    [SerializeField]
    private float _progressValue;

    [SerializeField]
    private ItemsManager _itemsManager;

    void Start()
    {
        _playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();
        _progressBarSlider = ProgressBar.GetComponent<Slider>();
        _piratesOnIsland = new List<Pirate>();
        _itemsManager = GameObject.Find("ItemsManager").GetComponent<ItemsManager>();

        InfoText.GetComponent<TextMeshProUGUI>().text = $"{IslandType} Island \n\n Only {Capacity} pirates allowed";

        _slots = new GameObject[8];

        for (int i = 0; i < _slots.Length; i++)
            _slots[i] = transform.Find($"Canvas/PirateSelectionPanel/Slots/Slot {i + 1}").gameObject;
    }

    void Update()
    {
        if (_hasActivityStarted)
        {
            _progressBarSlider.value += 0.05f * Time.deltaTime;

            if (_progressBarSlider.value == _progressBarSlider.maxValue)
            {
                _hasActivityStarted = false;
                CollectButton.SetActive(true);
            }
        }

        //Todo - Cooldown Algo
    }

    private void GetProgressValue()
    {
        _currentIdleTime = IdleTime;

        float miningPoints = _piratesOnIsland.Select(x => x.MiningPoints).Sum();

        Debug.Log($"Mining Points: {miningPoints}");

        if (miningPoints > IdleTime)
            _currentIdleTime = 1;
        else
            _currentIdleTime = IdleTime - miningPoints;

        _progressBarSlider.maxValue = _currentIdleTime;
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

        GetProgressValue();

        CloseAllPanels();

        _hasActivityStarted = true;
        ProgressBar.SetActive(true);
        JoinButton.SetActive(false);
    }

    public void OnCollectButtonPressed()
    {
        InfoPanel.SetActive(false);
        RewardsPanel.SetActive(true);
        CollectButton.SetActive(false);

        CollectRewards();
    }

    private void CollectRewards()
    {
        var rewards = _itemsManager.GetItems(MaxRewardsNumber, Region);
        var sb = new StringBuilder();

        foreach (var item in rewards) sb.Append($"{item.Key.Name} {item.Value}x | {item.Key.Rarity}\n");
        
        RewardsText.GetComponent<TextMeshProUGUI>().text = sb.ToString();

        _playerManagementController.StoreItems(rewards);
    }

    public void OnCloseButtonPressed()
    {
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
        RewardsPanel.SetActive(false);
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