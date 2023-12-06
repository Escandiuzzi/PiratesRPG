using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public IEnumerable<Pirate> Pirates { get; set; }

   public Difficulty Difficulty { get; set; }
   
   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
   }
}
