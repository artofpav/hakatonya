using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public float calories = 0.1f;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hero")) {
            GameManager.singl.EatFood(this);
        }
    }
    private void OnEnable() {
        print("Enable food");
    }


}
