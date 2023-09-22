using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    

    public Material mat1;
    public Material mat2;
    public MeshRenderer rend;

    public TileBasedController controller;

    private bool selected = false;

    
    public void ChangeMaterial() {
        selected = !selected;

        if (selected) {
            rend.material = mat2;
        } else {
            rend.material = mat1;
         }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
