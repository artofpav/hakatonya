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
    public List<Spawner> spawners;

    public CameraShake cameraShake;

    Ray ray;
    RaycastHit hit;

    private void Awake() {


        if (singl == null) {
            singl = this;
           // DontDestroyOnLoad(gameObject);

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

        for (int i = 0; i < spawners.Count; i++) {
            if (Vector3.Distance(spawners[i].transform.position, hero.transform.position) > spawners[i].radius + 5) {
                spawners[i].gameObject.SetActive(false);
            } else {
                spawners[i].gameObject.SetActive(true);
            }
        }

        hud.UpdateHUD();
    }

    public void HitHero(Enemy enemy) {
        float l = Mathf.Floor(hero.level);
        if (enemy.level >= l) {
            hero.Hit(enemy.level - l);
            StartCoroutine(cameraShake.Shake(.15f, .6f));
        } else {
            EatFood(enemy);
            enemy.gameObject.SetActive(false);
        }
    }

    public void EatFood(Food food) {
        hero.EatFood(food);
    }

    public void GameOver() {
        SceneManager.LoadScene("MainMenu");
    }
}
