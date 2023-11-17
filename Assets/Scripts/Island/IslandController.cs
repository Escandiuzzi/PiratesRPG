using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    public Region Region;

    public IslandType IslandType;

    public float IdleTime;

    public int Capacity;

    public int MaxRewardsNumber;

    public bool _hasActivityStarted;

    public GameObject InfoPanel;

    public GameObject PirateSelectionPanel;

    public GameObject RewardsPanel;

    public GameObject ProgressBar;

    public GameObject JoinButton;

    public GameObject CollectButton;

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

    void Start()
    {
        _playerManagementController = GameObject.Find("PlayerData").GetComponent<PlayerManagementController>();
        _progressBarSlider = ProgressBar.GetComponent<Slider>();
        _piratesOnIsland = new List<Pirate>();

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

        GetRewards();
    }

    private void GetRewards()
    {

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


/*
 func _getRewards():	
	randomize();
	var rewardN = randi() % maxRewards + 1;
	var rewards = {};
	for i in range(rewardN):
		#/////////////////////////////////////////
		var rarity = randi() % 100 + 1;
		var _item = item.instance();

		if rarity >= 90:
			var randReward = randi() % rareSize;
			_item._read_json_data(randReward, regionName, "Rare", islandType);
			_item._print_data();
		elif rarity >= 60 and rarity < 90:
			var randReward = randi() % uncommonSize;
			_item._read_json_data(randReward, regionName, "Uncommon", islandType);
			_item._print_data();
		else:
			var randReward = randi() % commonSize;
			_item._read_json_data(randReward, regionName, "Common", islandType);
			_item._print_data();
		
		if rewards.has(_item._get_name()):
			var item_count = rewards[_item._get_name()];
			item_count += 1;
			rewards[_item._get_name()] = item_count;
		else:
			rewards[_item._get_name()] = 1;
		
		#rewardText.text += _item._get_name();
		#rewardText.text += "\n";
		emit_signal("send_player_reward", _item);
		#/////////////////////////////////////////
		
	var keys = rewards.keys();
	for i in range(rewards.size()):
		rewardText.text += str(rewards[keys[i]]);
		rewardText.text += "x ";
		rewardText.text += keys[i];
		rewardText.text += "\n";
	
	pass;
 */