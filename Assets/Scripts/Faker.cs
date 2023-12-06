using System.Collections.Generic;
using UnityEngine;

public class Faker : MonoBehaviour
{
    public int numberOfPirates;
    public Difficulty difficulty;
    
    private GameManager _gameManager;
    
    private List<Pirate> _crew;
    
    void Start()
    {
        _crew = new List<Pirate>();
        
        AddFakeCrewData();
       
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.Difficulty = difficulty;
        _gameManager.Pirates = _crew;
    }
    
    private void AddFakeCrewData()
    {
        for (int i = 0; i < numberOfPirates; i++)
            _crew.Add(CreateFakePirate(0, "Test"));
    }

    private Pirate CreateFakePirate(int id, string name)
    {
        return new Pirate(id, name, true);
    }
}
