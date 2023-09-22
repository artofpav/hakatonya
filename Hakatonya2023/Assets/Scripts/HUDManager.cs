using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public Slider life;
    public Slider food;
    
    public Slider radiation;

    public Slider growth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHUD() {

        life.value = GameManager.singl.hero.life;
        food.value = GameManager.singl.hero.food;
        radiation.value = GameManager.singl.hero.radiation;

        growth.value = GameManager.singl.hero.level - Mathf.Floor(GameManager.singl.hero.level);
    }
}
