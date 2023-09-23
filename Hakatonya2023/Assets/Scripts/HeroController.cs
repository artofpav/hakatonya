using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public bool dontEat = false;
    public GameObject model;
    public float speed = 1;
    public float rotationSpeed = 90;
    public float maxSpeed;
    public bool rush = false;
    public float endurance = 1;
    public float enduranceLossSpeed = 0.1f;
    public bool isHidden = false;
    public float life = 1;


    public float food = 1;
    public float weight = 1;
    public float growLevel = 0.004f;
    public float level = 1;


    public float hungerSpeed = 0.01f;
    public float hungerDeathSpeed = 0.01f;

    public bool eating = false;
    public float eatMaxTime = 0.19f;
    public float eatingTime = 0;

    public bool naked = false;
    public float radiation = 0;
    public float radiationSpeed = 0.001f;


    public Animator animController;

    public float interactDistance = 2;
    public Food foodToEat;

    public HeroLevel currentLevel;
    public House currentHouse;

    public LayerMask goundColliderMask;
    public Transform sphereCastOrigin;
    public float sphereCastRadius = 2f;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private Quaternion toRotation;
    private Vector3 targetPosition;

    private RaycastHit hit;
    // note that the ray starts at 100 units
    private Ray ray;


    private float speedModifier = 1;
    private float damageModifier = 1;
    private float hungerModifier = 1;


    // Start is called before the first frame update
    void Start() {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        if (life <= 0) {

            level = Mathf.Floor(level) - 1;
            if (level <= 0) {
                GameManager.singl.GameOver();
                return;
            }
            life = 1;
            SetLevel(currentLevel.prevLevel);
        }


        if (!dontEat) {
            food -= hungerSpeed * Time.deltaTime * hungerModifier;
        }

        if (food > 1) {
            food = 1;
        } else if (food < 0) {
            life -= hungerDeathSpeed * Time.deltaTime;
            food = 0;
        }

        if (naked) {
            radiation += radiationSpeed; 
            life -= radiationSpeed * 10;
        }




        if (Mathf.Floor(level) > currentLevel.level && level < 6) {
            if (currentLevel.nextLevel != null) {
                SetLevel(currentLevel.nextLevel);
            }
        }

        if (!eating) {

            //hide cycle
            if (Input.GetKeyDown(KeyCode.Space)) {
                isHidden = true;
                animController.SetBool("hidden", true);
                currentLevel.dust.Stop();
                rBody.useGravity = false;
                rBody.velocity = new Vector3(0, 0, 0);
                currentLevel.coll.enabled = false;

                StartCoroutine(GameManager.singl.cameraShake.Shake(.5f, .15f));
            } else if (Input.GetKeyUp(KeyCode.Space)) {
                isHidden = false;
                animController.SetBool("hidden", false);
                currentLevel.coll.enabled = true;
                rBody.useGravity = true;

                StartCoroutine(GameManager.singl.cameraShake.Shake(.25f, .15f));
            }

            if (!isHidden) {

                vertical = Input.GetAxis("Vertical");
                horizontal = Input.GetAxis("Horizontal");
                direction = new Vector3(horizontal, 0, vertical);
                direction.Normalize();


                if (direction != Vector3.zero) {
                    animController.SetBool("walk", true);
                    currentLevel.dust.Play();
                    rBody.velocity = direction * speed * speedModifier * Time.deltaTime;

                    toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                } else {
                    currentLevel.dust.Stop();
                    animController.SetBool("walk", false);
                }


                if (horizontal > 0) {
                    rBody.velocity = new Vector3(horizontal * speed, rBody.velocity.y, rBody.velocity.z);
                } else if (horizontal < 0) {
                    rBody.velocity = new Vector3(horizontal * speed, rBody.velocity.y, rBody.velocity.z);
                } else {
                    rBody.velocity = new Vector3(0, rBody.velocity.y, rBody.velocity.z);
                }

                if (vertical > 0) {
                    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, vertical * speed);
                } else if (vertical < 0) {
                    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, vertical * speed);
                } else {
                    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, 0);
                }

            } else {
                currentLevel.dust.Stop();
            }


        } else { //finish eating
            currentLevel.dust.Stop();
            rBody.velocity = new Vector3(0, 0, 0);
            //= Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(foodToEat.transform.position, Vector3.up), rotationSpeed * (Time.deltaTime*10));
            eatingTime -= Time.deltaTime;
            if (eatingTime <= 0) {
                eating = false;
                animController.SetBool("eating", false);
                foodToEat.gameObject.transform.position = Vector3.zero;
                foodToEat.gameObject.SetActive(false);
            }
        }

        ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        targetPosition = transform.position;
        //        Vector3 rayCastOrigin = sphereCastOrigin.position;
        //        rayCastOrigin.y = rayCastOrigin.y + 0.2f;
        if (!isHidden) {
            if (Physics.SphereCast(sphereCastOrigin.position, sphereCastRadius, -Vector3.up, out hit, goundColliderMask)) {
                if (hit.collider != null & Vector3.Distance(hit.point, transform.position) < 0.2f) {
                    // this is where the gameobject is actually put on the ground
                    targetPosition.y = hit.point.y;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
                } else {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {
                        if (hit.collider != null) {
                            targetPosition.y = hit.point.y;
                            // this is where the gameobject is actually put on the ground
                            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
                        }
                    }
                }
            }
        }

    }

    internal void EatFood(Food _food) {
        float foodModifier = _food.level / level;
        level += (_food.calories * foodModifier) * (radiation + 1);
        foodToEat = _food;
        eating = true;
        animController.SetBool("eating", true);
        eatingTime = eatMaxTime;
        food += _food.calories;
    }

    public void Hit(float hitLevel) {
        animController.SetTrigger("damage");
        life -= (0.1f + (0.1f * hitLevel)) * damageModifier;
    }


    public void GetHouse(House _house) {

        currentHouse = _house;
        _house.transform.parent = currentLevel.houseHolder.transform;
        _house.transform.localPosition = Vector3.zero;
        _house.transform.localRotation = new Quaternion(0, 0, 0, 0);
        speedModifier = _house.speedModifier;
        damageModifier = _house.damageModifier;
        hungerModifier = _house.hungerModifier;


    }

    private void SetLevel(HeroLevel _targetLevel) {



        currentLevel.gameObject.SetActive(false);
        currentLevel.cam.SetActive(false);

        currentLevel = _targetLevel;

        currentLevel.gameObject.SetActive(true);
        currentLevel.cam.SetActive(true);

        if (currentHouse != null) {
            speedModifier = 1;
            damageModifier = 1;
            hungerModifier = 1;
            Destroy(currentHouse);
        }

        model = currentLevel.gameObject;
        animController = currentLevel.animController;

        speed = currentLevel.speed;
        enduranceLossSpeed = currentLevel.enduranceLossSpeed;
        growLevel = currentLevel.growLevel;
        hungerSpeed = currentLevel.hungerSpeed;
        hungerDeathSpeed = currentLevel.hungerDeathSpeed;
        eatMaxTime = currentLevel.eatMaxTime;
        radiationSpeed = currentLevel.radiationSpeed;

        sphereCastOrigin = currentLevel.sphereCastOrigin;
        sphereCastRadius = currentLevel.sphereCastRadius;




    }

}
