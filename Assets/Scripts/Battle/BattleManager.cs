using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private float aiThinkTime;
    
    [SerializeField] private GameObject gameManager;

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject[] playerGameObjects;

    [SerializeField] private Slider[] playerPirateHealthBars;
    
    [SerializeField] private GameObject[] aiGameObjects;
    
    [SerializeField] private Slider[] aiPirateHealthBars;
    
    private IList<Pirate> _playerPirates;
    
    private IList<Pirate> _aiPirates;

    private BattleButtonActionsHandler _battleButtonActions;
    
    private Difficulty _difficulty;

    private System.Random _random;
    
    private List<Pirate> _piratesInBattle;
    
    private Pirate _currentAttacker;

    private int _currentAttackerIndex;
    
    private bool _isPlayerTurn;
    
    void Start()
    {
        _random = new System.Random();
        _aiPirates = new List<Pirate>();
        _piratesInBattle = new List<Pirate>();
        
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        
        _playerPirates = gameManager.GetComponent<GameManager>().Pirates;
        _difficulty = gameManager.GetComponent<GameManager>().Difficulty;

        _battleButtonActions = gameObject.GetComponent<BattleButtonActionsHandler>();
        
        playerGameObjects = new GameObject[3];
        playerPirateHealthBars = new Slider[3];
        
        aiGameObjects = new GameObject[3];
        aiPirateHealthBars = new Slider[3];
        
        for (var i = 0; i < 3; i++)
        {
            playerGameObjects[i] = canvas.transform.Find($"Player Pirates Group/Pirate {i + 1}").gameObject;
            playerPirateHealthBars[i] = playerGameObjects[i].transform.Find("Health Bar").gameObject.GetComponent<Slider>();
            
            aiGameObjects[i] = canvas.transform.Find($"AI Pirates Group/Pirate {i + 1}").gameObject;
            aiPirateHealthBars[i] = aiGameObjects[i].transform.Find("Health Bar").gameObject.GetComponent<Slider>();
        }

        BuildScene();
    }

    private void BuildScene()
    {
        for (var i = 0; i < _playerPirates.Count(); i++)
        {
            playerGameObjects[i].SetActive(true);
        }

        GenerateAIEnemies();
        StartBattle();
    }
    private void GenerateAIEnemies()
    {
        var numberOfEnemies = _difficulty switch
        {
            Difficulty.Easy => 1,
            Difficulty.Medium => 2,
            Difficulty.Hard => 3,
            _ => 1
        };
        
        _battleButtonActions.SetAvailableEnemyButtons(numberOfEnemies);

        for (var i = 0; i < numberOfEnemies; i++)
        {
            _aiPirates.Add( new Pirate(i, "", true, true));
            aiGameObjects[i].SetActive(true);
        }
    }
    
    private void StartBattle()
    {
        _currentAttacker = _playerPirates.First();
        _isPlayerTurn = true;
        _currentAttackerIndex = 0;
        
        _piratesInBattle.AddRange(_playerPirates);
        _piratesInBattle.AddRange(_aiPirates);
    }

    private void NextTurn()
    {
        _currentAttackerIndex++;

        if (_currentAttackerIndex >= _piratesInBattle.Count) _currentAttackerIndex = 0;

        _currentAttacker = _piratesInBattle[_currentAttackerIndex];

        _isPlayerTurn = !_currentAttacker.IsAIControlled;

        _battleButtonActions.HideAllPanels();
        
        if (_isPlayerTurn)
            _battleButtonActions.SetActionsPanel();
        
        else
            StartCoroutine(ExecuteAIAttack());
    }
    
    IEnumerator ExecuteAIAttack()
    {
        var enemyIndex = _random.Next(_playerPirates.Count);
        var pirate = _playerPirates[enemyIndex];
        
        yield return new WaitForSeconds(aiThinkTime);

        ExecuteAttack(pirate, _currentAttacker.AttackingPoints);
        UpdateHealthBar(enemyIndex, false);
        
        _battleButtonActions.SetActionsPanel();
    }

    public void ExecutePlayerAttack(int index)
    {
        var enemy = _aiPirates[index];
        ExecuteAttack(enemy, _currentAttacker.AttackingPoints);
        UpdateHealthBar(index, true);
        
        NextTurn();
    }

    private void UpdateHealthBar(int index, bool updateEnemyHb)
    {
        Pirate pirate;
        Slider healthBar;
        
        if (updateEnemyHb)
        {
            pirate = _aiPirates[index];
            healthBar = aiPirateHealthBars[index];
        }
        else
        {
            pirate = _playerPirates[index];
            healthBar = playerPirateHealthBars[index];
        } 
        
        var value = (float)pirate.Hp / (float)pirate.MaxHp;
        healthBar.value = value;
    }

    private void ExecuteAttack(Pirate attackedPirate, int damage)
    {
        attackedPirate.Hp -= damage;
    }

    public IList<Special> GetAttackerSpecialAttacks() => _currentAttacker.Specials.ToList();
}
