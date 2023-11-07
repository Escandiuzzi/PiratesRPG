using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManagementController : MonoBehaviour
{
    [SerializeField]
    private List<Pirate> _crew;
    
    void Start()
    {
        _crew = new List<Pirate>();

        AddFakeCrewData();
    }

    public Pirate GetPirateById(int id) => _crew.First(x => x.Id == id);

    public List<Pirate> GetCrew() => _crew;

    private void AddFakeCrewData()
    {
        _crew.Add(CreateFakePirate(0, "Luiz"));
        _crew.Add(CreateFakePirate(1, "Felipe"));
        _crew.Add(CreateFakePirate(2, "Escandiuzzi"));
    }

    private Pirate CreateFakePirate(int id, string name) => new(id, name, true);
    
}
