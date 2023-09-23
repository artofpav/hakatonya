using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float level = 1;

    public Collider coll;

    public float speedModifier = 1;
    public float damageModifier = 1;
    public float hungerModifier = 1;

    private void OnTriggerEnter(Collider other) {
        
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hero")) {
            if (Mathf.Floor(GameManager.singl.hero.level) == level) {
                GameManager.singl.hero.GetHouse(this);
            } else {
                Physics.IgnoreCollision(GameManager.singl.hero.GetComponent<Collider>(), GetComponent<Collider>());

            }
        }
    }
}
