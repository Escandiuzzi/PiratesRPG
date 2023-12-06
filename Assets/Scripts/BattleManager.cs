using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject[] playerGameObjects;

    [SerializeField] private GameObject[] aiGameObjects;
    
    private IEnumerable<Pirate> _playerPirates;
    private IList<Pirate> _aiPirates;
    
    private Difficulty _difficulty;

    private System.Random _random;
    
    void Start()
    {
        _random = new System.Random();
        _aiPirates = new List<Pirate>();
        
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        
        _playerPirates = gameManager.GetComponent<GameManager>().Pirates;
        _difficulty = gameManager.GetComponent<GameManager>().Difficulty;
        
        playerGameObjects = new GameObject[3];
        aiGameObjects = new GameObject[3];
        
        for (var i = 0; i < 3; i++)
        {
            playerGameObjects[i] = canvas.transform.Find($"Player Pirates Group/Pirate {i + 1}").gameObject;
            aiGameObjects[i] = canvas.transform.Find($"AI Pirates Group/Pirate {i + 1}").gameObject;
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

        for (var i = 0; i < numberOfEnemies; i++)
        {
            _aiPirates.Add( new Pirate(i, "", true));
            aiGameObjects[i].SetActive(true);
        }
    }
}
