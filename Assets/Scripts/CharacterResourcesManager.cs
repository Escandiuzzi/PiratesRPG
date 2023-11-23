using System.Collections.Generic;
using UnityEngine;

public class CharacterResourcesManager : MonoBehaviour
{
    [SerializeField] public List<Sprite[]> Resources;

    public void Start()
    {
        Resources = new List<Sprite[]>();
    }
}