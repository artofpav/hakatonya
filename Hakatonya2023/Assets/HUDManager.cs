using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public Slider life;
    public Slider food;
    public Slider water;
    public Slider radiation;

    public HeroController hero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHUD() {

        life.value = hero.life;
        food.value = hero.food;

        radiation.value = hero.radiation;
    }
}