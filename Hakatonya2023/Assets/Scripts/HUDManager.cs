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

    public GameObject speedUP;
    public GameObject speedDown;
    public GameObject armorUp;
    public GameObject armorDown;
    public GameObject foodUp;
    public GameObject foodDown;

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

        if (GameManager.singl.hero.damageModifier > 1) {
            foodDown.SetActive(true);
            foodUp.SetActive(false);
        } else if (GameManager.singl.hero.damageModifier < 1) {
            foodDown.SetActive(false);
            foodUp.SetActive(true);
        } else {
            foodDown.SetActive(false);
            foodUp.SetActive(false);
        }

        if (GameManager.singl.hero.hungerModifier > 1) {
            armorDown.SetActive(true);
            armorUp.SetActive(false);
        } else if (GameManager.singl.hero.hungerModifier < 1) {
            armorDown.SetActive(false);
            armorUp.SetActive(true);
        } else {
            armorDown.SetActive(false);
            armorUp.SetActive(false);
        }

        if (GameManager.singl.hero.speedModifier > 1) {
            speedUP.SetActive(true);
            speedDown.SetActive(false);
        } else if (GameManager.singl.hero.speedModifier < 1) {
            speedUP.SetActive(false);
            speedDown.SetActive(true);
        } else {
            speedUP.SetActive(false);
            speedDown.SetActive(false);
        }

    }
}
