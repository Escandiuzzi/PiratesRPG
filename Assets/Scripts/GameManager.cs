using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public int numberOfPirates;
   public Difficulty difficulty;
   
   public IList<Pirate> Pirates { get; set; }

   public Difficulty Difficulty
   {
      get => difficulty;
      set => difficulty = value;
   }
   
   private void Awake()
   {
      Pirates = new List<Pirate>();
      AddFakeCrewData();
      DontDestroyOnLoad(gameObject);
   }
   
   private  void AddFakeCrewData()
   {
      for (var i = 0; i < numberOfPirates; i++)
         Pirates.Add(CreateFakePirate(0, "Test"));
   }

   private Pirate CreateFakePirate(int id, string name) => new Pirate(id, name, true);
}
