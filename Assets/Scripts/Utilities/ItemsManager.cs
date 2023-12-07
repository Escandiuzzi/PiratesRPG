using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    private Random _random;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var filePath = Path.Combine(Application.dataPath, "Resources/Json Files/items.json");

        if (File.Exists(filePath))
        {
            var jsonContent = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(jsonContent))
            {
                var itemsData = JsonUtility.FromJson<ItemsData>(jsonContent);

                foreach (var parsedItem in itemsData.data)
                {
                    var item = new Item(
                        parsedItem.id,
                        parsedItem.name,
                        (ItemType)parsedItem.type,
                        parsedItem.heal, parsedItem.damage,
                        parsedItem.durability,
                        (Rarity)parsedItem.rarity,
                        (Region)parsedItem.region
                    );

                    _items.Add(item);
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

        _random = new Random();
    }

    public IDictionary<Item, int> GetItems(int numberOfItems, Region itemRegion)
    {
        var itemsDropped = 0;
        var droppableItems = _items.Where(x => x.Region == itemRegion).ToList();

        var rareItems = droppableItems.Where(x => x.Rarity == Rarity.Rare).ToList();
        var uncommonItems = droppableItems.Where(x => x.Rarity == Rarity.Uncommon).ToList();
        var commonItems = droppableItems.Where(x => x.Rarity == Rarity.Common).ToList();

        var items = new Dictionary<Item, int>();

        while (itemsDropped < numberOfItems)
        {
            var rarity = SortItemRarity();
            var item = rarity switch
            {
                Rarity.Rare => GetItemByRarity(rareItems),
                Rarity.Uncommon => GetItemByRarity(uncommonItems),
                _ => GetItemByRarity(commonItems)
            };

            var availableQuantity = numberOfItems - itemsDropped;
            var quantity = _random.Next(1, availableQuantity);

            if (items.ContainsKey(item)) items[item] += quantity;
            else items.Add(item, quantity);

            itemsDropped += quantity;
        }

        return items;
    }

    private Item GetItemByRarity(IList<Item> items)
    {
        var index = _random.Next(items.Count());

        return items[index];
    }

    private Rarity SortItemRarity()
    {
        var rand = new Random();
        var sortedNumber = rand.Next(101);

        return sortedNumber switch
        {
            // Ultra Rare
            >= 95 => Rarity.Rare,
            // Rare
            > 87 and < 95 => Rarity.Uncommon,
            // Common
            _ => Rarity.Common
        };
    }
}

[Serializable]
public class JsonItem
{
    public int id;
    public int damage;
    public int durability;
    public int heal;
    public string name;
    public int type;
    public int rarity;
    public int region;
}

[Serializable]
public class ItemsData
{
    public List<JsonItem> data;
}