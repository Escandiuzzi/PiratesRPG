using System.Collections.Generic;
using Models;

public class Pirate
{
    // Weapon { private set; get; }
    // Shield { private set; get; }

    public Pirate(int id, string name, bool initialize, bool isAIControlled = false)
    {
        Id = id;
        Name = name;
        IsAIControlled = isAIControlled;
        
        if (initialize) InitializePirate();
    }

    public int Id { private set; get; }

    public string Name { private set; get; }

    private int _hp;
    public int Hp {
        set
        {
            _hp = value;
            if (_hp < 0) _hp = 0;
        }
        get => _hp;
    }
    
    public int MaxHp { private set; get; }

    public int Energy { private set; get; }

    public int MaxEnergy { private set; get; }

    public int AttackingPoints { private set; get; }

    public int MiningPoints { private set; get; }

    public int CookingPoints { private set; get; }

    public IEnumerable<Special> Specials { private set; get; }

    public bool IsAIControlled { private set; get; }
    
    public bool IsBusy { set; get; }

    private void InitializePirate()
    {
        var hpStat = GenerateStat(18, 8, 11, 6, 9, 8);
        Hp = hpStat;
        MaxHp = hpStat;

        var energyStat = GenerateStat(18, 8, 11, 6, 9, 6);
        Energy = energyStat;
        MaxEnergy = energyStat;

        AttackingPoints = GenerateStat(7, 3, 5, 2, 4, 1);
        MiningPoints = GenerateStat(7, 3, 5, 2, 4, 1);
        CookingPoints = GenerateStat(7, 3, 5, 2, 4, 1);

        Specials = SpecialAttackManager.GetSpecials();
    }

    private int GenerateStat(int ultraRareBase, int ultraRareAdd, int rareBase, int rareAdd, int commonBase,
        int commonAdd)
    {
        var rand = new System.Random();
        var sortedNumber = rand.Next(101);

        return sortedNumber switch
        {
            // Ultra Rare
            >= 95 => rand.Next(ultraRareBase) + ultraRareAdd,
            // Rare
            > 87 and < 95 => rand.Next(rareBase) + rareAdd,
            // Common
            _ => rand.Next(commonBase) + commonAdd
        };
    }
}