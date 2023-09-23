using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public List<GameObject> cuts;
    private int count = 0;


    private void Start() {
        foreach (GameObject cut in cuts) {
            cut.SetActive(false);
        }

        cuts[0].SetActive(true);
    }

    private void Update() {


        if (Input.GetKeyDown(KeyCode.Space)) {
            if (count < cuts.Count - 1) {
                cuts[count].SetActive(false);
                count++;
                cuts[count].SetActive(true);
            } else {
                SceneManager.LoadScene("MainGame");
            }
        }
    }

}
