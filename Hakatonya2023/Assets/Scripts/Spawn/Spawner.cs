using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float radius = 10;
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
                spawnPosition = new Vector3(Random.insideUnitCircle.x * areas[selectedArea].radius, 0, Random.insideUnitCircle.y * areas[selectedArea].radius);

                itemsToSpawn[i].transform.position = spawnPosition+ areas[selectedArea].transform.position ;
                itemsToSpawn[i].transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                itemsToSpawn[i].SetActive(true);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
