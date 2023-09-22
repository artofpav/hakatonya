using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Food
{

    public float level;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hero")) {

            GameManager.singl.HitHero(this);
        }
    }
}
