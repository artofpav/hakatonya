using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float level = 1;

    public Collider coll;


    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hero")) {

            GameManager.singl.hero.GetHouse(this);
        }
    }
}
