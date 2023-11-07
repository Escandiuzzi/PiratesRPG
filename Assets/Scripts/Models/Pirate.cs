using System;

public class Pirate
{
    public int Id { private set; get; }

    public string Name { private set; get; }

    public int Hp { private set; get; }

    public int MaxHp { private set; get; }

    public int Energy { private set; get; }

    public int MaxEnergy { private set; get; }

    public bool Busy { private set; get; }

    public int AttackingPoints { private set; get; }

    public int MiningPoints { private set; get; }

    public int CookingPoints { private set; get; }

    public int[] SpecialAttackIds { private set; get; }

    // Weapon { private set; get; }
    // Shield { private set; get; }

    public Pirate(int id, string name, bool initialize)
    {
        Id = id;
        Name = name;
        SpecialAttackIds = new int[4];

        if (initialize) InitializePirate();
    }

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

       for (int i = 0; i < 4; i ++) SpecialAttackIds[i] = UnityEngine.Random.Range(0, 4);
    }

    private int GenerateStat(int ultraRareBase, int ultraRareAdd, int rareBase, int rareAdd, int commonBase, int commonAdd)
    {
        var rand = new Random();
        var sortedNumber = rand.Next(101);

        return sortedNumber switch
        {
            // Ultra Rare
            >= 95 => rand.Next(ultraRareBase) + ultraRareAdd,
            // Rare
            > 87 and < 95 => rand.Next(rareBase) + rareAdd,
            // Common
            _ => rand.Next(commonBase) + commonAdd,
        };
    }    
}
