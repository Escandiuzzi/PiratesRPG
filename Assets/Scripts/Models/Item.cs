using System;

[Serializable]
public class Item
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public ItemType Type { get; private set; }

    public int Heal { get; private set; }

    public int Damage { get; private set; }

    public int Durability { get; private set; }

    public Rarity Rarity { get; private set; }

    public Region Region { get; private set; }

    public Item(int id, string name, ItemType type, int heal, int damage, int durability, Rarity rarity, Region region)
    {
        Id = id;
        Name = name;
        Type = type;
        Heal = heal;
        Damage = damage;
        Durability = durability;
        Rarity = rarity;
        Region = region;
    }
}

public enum ItemType
{
    Consumable,
    Material,
    Weapon,
    Armor
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare
}

public enum Region
{
    South,
    Southeast,
    Northeast,
    North
}