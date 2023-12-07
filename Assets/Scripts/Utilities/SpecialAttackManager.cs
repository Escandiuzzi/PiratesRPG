using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using UnityEngine;
using Random = System.Random;

public static class SpecialAttackManager
{
    private static Random _random = new Random();
    
    private static List<Special> _specials = new List<Special>();

    public static IEnumerable<Special> GetSpecials()
    {
        if (_specials.Count == 0) GetSpecialsFromFile();   
        
        var _selectedSpecials = new List<Special>();

        var numberOfSpecials = _random.Next(1, 4);

        var availableSpecials = _specials.ToList();
        
        for (var i = 0; i < numberOfSpecials; i++)
        {
            var index = _random.Next(0, availableSpecials.Count - 1);

            var special = availableSpecials.ElementAt(index);
            
            _selectedSpecials.Add(special);
            availableSpecials.RemoveAt(index);
        }
        
        return _selectedSpecials;
    }

    private static void GetSpecialsFromFile()
    {
        var filePath = Path.Combine(Application.dataPath, "Resources/Json Files/specials.json");

        if (File.Exists(filePath))
        {
            var jsonContent = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(jsonContent))
            {
                var specialsData = JsonUtility.FromJson<SpecialData>(jsonContent);

                foreach (var parsedItem in specialsData.data)
                {
                    var special = new Special(
                        parsedItem.name,
                        parsedItem.damage,
                        parsedItem.heal, 
                        parsedItem.range,
                        parsedItem.energy
                    );

                    _specials.Add(special);
                }
            }
            else
            {
                Debug.LogError("JSON content is empty!");
            }
        }
        
        else
        {
            Debug.LogError("File not found at path: " + filePath);
        }
    }
}

[Serializable]
public class JsonSpecial
{
    public string name;
    public int damage;
    public int heal;
    public int range;
    public int energy;
}

[Serializable]
public class SpecialData
{
    public List<JsonSpecial> data;
}