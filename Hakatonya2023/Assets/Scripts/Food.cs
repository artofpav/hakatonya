using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public float calories = 0.1f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Hero")) {
            GameManager.singl.EatFood(this);
            gameObject.SetActive(false);
        //}
    }


}
