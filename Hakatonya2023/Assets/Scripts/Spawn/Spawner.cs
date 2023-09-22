using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public List<SpawnArea> areas;

    [SerializeField]
    public List<GameObject> itemsToSpawn;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < itemsToSpawn.Count; i++) {
            if (!itemsToSpawn[i].activeSelf) { 
                
            }
        }
    }
}
