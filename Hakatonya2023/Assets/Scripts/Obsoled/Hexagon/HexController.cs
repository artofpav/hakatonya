using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
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
                map[i, j].transform.position = GetHexPosition(i, j);
            }
        }
    }

    private Vector3 GetHexPosition(int i, int j) {

        bool shouldOffset = false;
        float offset;

        shouldOffset = (i % 2) == 0;
        float width = Mathf.Sqrt(3) * (tileSize);
        float height = 2 * (tileSize);
        height *= 3f/4f;

        offset = shouldOffset ? width / 2 : 0;


        return new Vector3((j * width + offset) - mapSize/2, UnityEngine.Random.Range(0, 0.2f), (i * height) - mapSize/2);

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
