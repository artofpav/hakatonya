using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBasedController : MonoBehaviour
{

    public Tile tilePrefab;
    public float tileSize = 1;

    public int mapSize = 5;

    private  Tile[,] map;



    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        map = new Tile[mapSize, mapSize];
        float offset = mapSize;
        offset /= 2;
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                map[i, j] = Instantiate(tilePrefab);
                map[i, j].transform.SetParent(transform);
                map[i, j].transform.position = new Vector3((i * tileSize)-offset, 0, (j * tileSize)-offset);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (Input.GetMouseButtonDown(0))
                //print(hit.collider.name);
                hit.collider.GetComponent<Tile>().ChangeMaterial();
        }
    }
}
