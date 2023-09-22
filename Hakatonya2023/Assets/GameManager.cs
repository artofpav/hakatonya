using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    // static reference to singleton GameManager instance
    public static GameManager singl;

    public HUDManager hud;
    public HeroController hero;

    Ray ray;
    RaycastHit hit;

    private void Awake() {


        if (singl == null) {
            singl = this;
            DontDestroyOnLoad(gameObject);

        } else if (singl != this) {
            Destroy(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD() {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (Input.GetMouseButtonDown(0))
                //print(hit.collider.name);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Food")) {
                    if (Vector3.Distance(hero.transform.position, hit.collider.transform.position) <= hero.interactDistance) {
                        hero.food += hit.collider.GetComponent<Food>().calories;
                        Destroy(hit.collider.gameObject);
                    }
                }
                
        }

        hud.UpdateHUD();
    }

    public void HitHero(Enemy enemy) {
        if (enemy.level >= hero.level) {
            hero.Hit(enemy.level - hero.level);
        }
    }

    public void GameOver() {
        SceneManager.LoadScene("MainMenu");
    }
}
