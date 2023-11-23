using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManagementController : MonoBehaviour
{
    [SerializeField] private List<Pirate> _crew;

    [SerializeField] private Dictionary<Item, int> _inventory;

    private void Start()
    {
        _crew = new List<Pirate>();
        _inventory = new Dictionary<Item, int>();

        AddFakeCrewData();
    }

    public IDictionary<Item, int> GetInventory()
    {
        return _inventory;
    }

    public Pirate GetPirateById(int id)
    {
        return _crew.First(x => x.Id == id);
    }

    public IList<Pirate> GetCrew()
    {
        return _crew;
    }

    public IList<Pirate> GetAvailablePirates()
    {
        return _crew.Where(x => x.IsBusy == false).ToList();
    }

    private void AddFakeCrewData()
    {
        _crew.Add(CreateFakePirate(0, "John"));
        _crew.Add(CreateFakePirate(1, "Bonny"));
        _crew.Add(CreateFakePirate(2, "Morgan"));
    }

    private Pirate CreateFakePirate(int id, string name)
    {
        return new Pirate(id, name, true);
    }

    public void StoreItems(IDictionary<Item, int> rewards)
    {
        foreach (var item in rewards)
            if (_inventory.ContainsKey(item.Key)) _inventory[item.Key] += item.Value;
            else _inventory.Add(item.Key, item.Value);
    }
}