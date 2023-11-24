using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    public Region region;

    public IslandType islandType;

    public float idleTime;

    public int capacity;

    public int maxRewardsNumber;

    public bool renewable;
        
    public int renewWaitTime;
   
    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private GameObject pirateSelectionPanel;

    [SerializeField]
    private GameObject rewardsPanel;

    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject joinButton;

    [SerializeField]
    private GameObject collectButton;

    [SerializeField]
    private GameObject rewardsText;

    [SerializeField]
    private GameObject infoText;

    private GameObject _player;

    private PlayerMovementController _playerMovementController;
    
    [SerializeField] 
    private GameObject[] slots;

    [SerializeField] 
    private float currentIdleTime;

    [SerializeField] private Button button;

    [SerializeField] private Slider progressBarSlider;

    [SerializeField] private PlayerManagementController playerManagementController;

    [SerializeField] private float progressValue;

    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private bool hasActivityStarted;
    
    [SerializeField] private bool hasRenewingStarted; 

    private List<Pirate> _piratesOnIsland;

    private int _piratesSelected;
    
    private void Start()
    {
        playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();
        infoPanel = transform.Find("Canvas/IslandInfoPanel").gameObject;
        pirateSelectionPanel = transform.Find("Canvas/PirateSelectionPanel").gameObject;
        rewardsPanel = transform.Find("Canvas/RewardsPanel").gameObject;
        progressBar = infoPanel.transform.Find("ProgressBar").gameObject;
        joinButton = infoPanel.transform.Find("JoinButton").gameObject;
        collectButton = infoPanel.transform.Find("CollectButton").gameObject;
        infoText = infoPanel.transform.Find("InfoText").gameObject;
        rewardsText = rewardsPanel.transform.Find("ItemsText").gameObject;
        
        progressBarSlider = progressBar.GetComponent<Slider>();
        _piratesOnIsland = new List<Pirate>();
        itemsManager = GameObject.Find("ItemsManager").GetComponent<ItemsManager>();
        _player = GameObject.Find("Player");
        _playerMovementController = _player.GetComponent<PlayerMovementController>();
        
        infoText.GetComponent<TextMeshProUGUI>().text = $"{islandType} Island \n\n Only {capacity} pirates allowed";

        slots = new GameObject[8];

        for (var i = 0; i < slots.Length; i++)
            slots[i] = transform.Find($"Canvas/PirateSelectionPanel/Slots/Slot {i + 1}").gameObject;
    }

    private void Update()
    {
        if (hasActivityStarted)
        {
            progressBarSlider.value += 0.05f * Time.deltaTime;

            if (progressBarSlider.value == progressBarSlider.maxValue)
            {
                hasActivityStarted = false;
                collectButton.SetActive(true);
            }
        }

        if (hasRenewingStarted)
        {
            progressBarSlider.value -= 0.05f * Time.deltaTime;

            if (progressBarSlider.value <= 0)
            {
                progressBar.SetActive(false);
                joinButton.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        infoPanel.SetActive(true);
        _playerMovementController.EnteredInIslandRange();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseAllPanels();
    }

    private void GetProgressValue()
    {
        currentIdleTime = idleTime;

        float miningPoints = _piratesOnIsland.Select(x => x.MiningPoints).Sum();

        Debug.Log($"Mining Points: {miningPoints}");

        if (miningPoints > idleTime)
            currentIdleTime = 1;
        else
            currentIdleTime = idleTime - miningPoints;

        progressBarSlider.maxValue = currentIdleTime;
    }

    public void OnJoinButtonClicked()
    {
        infoPanel.SetActive(false);
        pirateSelectionPanel.SetActive(true);

        SetPirateSelectionPanel();
    }

    private void SetPirateSelectionPanel()
    {
        HideSlots();

        var crew = playerManagementController.GetAvailablePirates();

        for (var i = 0; i < crew.Count; i++)
        {
            var pirate = crew[i];

            var slot = slots[i];
            slot.SetActive(true);
            slot.GetComponent<SlotToggleScript>().SetPirate(pirate);
        }
    }

    public void OnStartButtonPressed()
    {
        if (_piratesSelected == 0) return;
        
        if (islandType == IslandType.Battle) SceneManager.LoadScene(1);
        
        foreach (var slot in slots)
            if (slot.GetComponent<Toggle>().isOn)
            {
                var pirate = slot.GetComponent<SlotToggleScript>().GetPirate();
                pirate.IsBusy = true;

                _piratesOnIsland.Add(pirate);
            }

        GetProgressValue();

        CloseAllPanels();

        hasActivityStarted = true;
        progressBar.SetActive(true);
        joinButton.SetActive(false);
    }

    public void OnCollectButtonPressed()
    {
        infoPanel.SetActive(false);
        rewardsPanel.SetActive(true);
        collectButton.SetActive(false);

        CollectRewards();

        foreach (var pirate in _piratesOnIsland) pirate.IsBusy = false;
        _piratesOnIsland.Clear();
        _piratesSelected = 0;
    }

    private void CollectRewards()
    {
        var rewards = itemsManager.GetItems(maxRewardsNumber, region);
        var sb = new StringBuilder();

        foreach (var item in rewards) sb.Append($"{item.Key.Name} {item.Value}x | {item.Key.Rarity}\n");

        rewardsText.GetComponent<TextMeshProUGUI>().text = sb.ToString();

        playerManagementController.StoreItems(rewards);

        if (renewable) hasRenewingStarted = true;
    }

    public void OnCloseButtonPressed()
    {
        CloseAllPanels();
    }

    private void CloseAllPanels()
    {
        infoPanel.SetActive(false);
        pirateSelectionPanel.SetActive(false);
        rewardsPanel.SetActive(false);
    }

    private void HideSlots()
    {
        foreach (var slot in slots) slot.SetActive(false);
    }

    public bool TryAddPirate()
    {
        if (_piratesSelected >= capacity) return false;
        
        _piratesSelected++;
        return true;
    }
}

public enum IslandType
{
    Mining = 0,
    Exploration = 1,
    Battle = 2
}