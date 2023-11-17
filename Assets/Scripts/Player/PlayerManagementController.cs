using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManagementController : MonoBehaviour
{
    [SerializeField]
    private List<Pirate> _crew;

    [SerializeField]
    private List<Item> _inventory;

    void Start()
    {
        _crew = new List<Pirate>();

        AddFakeCrewData();
    }

    public Pirate GetPirateById(int id) => _crew.First(x => x.Id == id);

    public IList<Pirate> GetCrew() => _crew;

    public IList<Pirate> GetAvailablePirates() => _crew.Where(x => x.IsBusy == false).ToList();

    public void LootRewards(Item[] items)
    {
        _inventory.AddRange(items);
    }

    private void AddFakeCrewData()
    {
        _crew.Add(CreateFakePirate(0, "John"));
        _crew.Add(CreateFakePirate(1, "Bonny"));
        _crew.Add(CreateFakePirate(2, "Morgan"));
    }

    private Pirate CreateFakePirate(int id, string name) => new(id, name, true);
}
