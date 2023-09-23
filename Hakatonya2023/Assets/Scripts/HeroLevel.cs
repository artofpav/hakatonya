using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLevel : MonoBehaviour
{
    public float level = 1;

    public HeroLevel prevLevel;
    public HeroLevel nextLevel;

    public Animator animController;

    public float speed = 1;
    public float enduranceLossSpeed = 1;
    public float growLevel = 0.004f;
    public float hungerSpeed = 0.01f;
    public float hungerDeathSpeed = 0.01f;
    public float eatMaxTime = 0.19f;
    public float radiationSpeed = 0.001f;

    public Transform sphereCastOrigin;
    public float sphereCastRadius = 2f;


    public Collider coll;
    
    public GameObject houseHolder;
    public GameObject cam;
    public ParticleSystem dust;

}
