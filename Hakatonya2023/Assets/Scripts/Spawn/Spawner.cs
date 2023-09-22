using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public List<SpawnArea> areas;

    [SerializeField]
    public List<GameObject> itemsToSpawn;

    private Vector3 spawnPosition;
    private int selectedArea;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < itemsToSpawn.Count; i++) {
            if (!itemsToSpawn[i].activeSelf) {
                selectedArea = Random.Range(0, areas.Count);
                spawnPosition = Random.insideUnitCircle * areas[selectedArea].radius;

                itemsToSpawn[i].transform.position = spawnPosition+ areas[selectedArea].transform.position ;
                itemsToSpawn[i].SetActive(true);
            }
        }
    }
}
