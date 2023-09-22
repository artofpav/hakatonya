using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject model;
    public float speed = 1 ;
    public float rotationSpeed = 90;
    public float maxSpeed;
    public bool rush = false;
    public float endurance = 1;
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

    public LayerMask goundColliderMask;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private Quaternion toRotation;

    private RaycastHit hit;
    // note that the ray starts at 100 units
    private Ray ray;



    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        if (life <= 0) {
            GameManager.singl.GameOver();
        }

        food -= hungerSpeed * Time.deltaTime;

        if (food > 1) {
            food = 1;
        } else if (food < 0) {
            life -= hungerDeathSpeed * Time.deltaTime;
        }

        if (naked) {
            radiation += radiationSpeed; //TODO ��� ��������� ���������
            life -= radiationSpeed * 10;
        }

        level += growLevel * (radiation + 1);

        if (!eating) {

            //hide cycle
            if (Input.GetKeyDown(KeyCode.Space)) {
                isHidden = true;
                animController.SetBool("hidden", true);
                rBody.useGravity = false;
                rBody.velocity = new Vector3(0, 0, 0);
                currentLevel.coll.enabled = false;
            } else if (Input.GetKeyUp(KeyCode.Space)) {
                isHidden = false;
                animController.SetBool("hidden", false);
                currentLevel.coll.enabled = true;
                rBody.useGravity = true;
            }

            if (!isHidden) {

                vertical = Input.GetAxis("Vertical");
                horizontal = Input.GetAxis("Horizontal");
                direction = new Vector3(horizontal, 0, vertical);
                direction.Normalize();


                if (direction != Vector3.zero) {
                    animController.SetBool("walk", true);
                    rBody.velocity = direction * speed * Time.deltaTime;

                    toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                } else {
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

            }


        } else { //finish eating

                //= Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(foodToEat.transform.position, Vector3.up), rotationSpeed * (Time.deltaTime*10));
            eatingTime -= Time.deltaTime;
            if (eatingTime <= 0) {
                eating = false;
                animController.SetBool("eating", false);
                foodToEat.gameObject.SetActive(false);
            }
        }

        ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {
            if (hit.collider != null & Vector3.Distance(hit.point, transform.position) > 0.2f) {
                // this is where the gameobject is actually put on the ground
                transform.position = new Vector3(transform.position.x, hit.point.y+0.2f, transform.position.z);
            }
        }

    }

    internal void EatFood(Food _food) {
        foodToEat = _food;
        eating = true;
        animController.SetBool("eating", true);
        eatingTime = eatMaxTime;
        food += _food.calories;
    }

    public void Hit(float hitLevel) {
        animController.SetTrigger("damage");
        life -= 0.1f + (0.1f * hitLevel);
    }
    

    public void GetHouse (House _house) {

        _house.transform.parent = currentLevel.houseHolder.transform;
        _house.transform.localPosition = Vector3.zero;
        _house.transform.localRotation = new Quaternion(0,0,0,0);

    }

}
