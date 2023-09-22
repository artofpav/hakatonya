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


    public bool naked = false;
    public float radiation = 0;
    public float radiationSpeed = 0.001f;


    public Animator animController;

    public float interactDistance = 2;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    Quaternion toRotation;



    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
            radiation += radiationSpeed; //TODO нет максимума раддиации
            life -= radiationSpeed * 10;
        }

        level += growLevel *(radiation+1);



        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();


        if (direction != Vector3.zero) {
            rBody.velocity = direction * speed * Time.deltaTime;

            toRotation = Quaternion.LookRotation(direction, Vector3.up);
            model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
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

    public void Hit(float hitLevel) {
        life -= 0.1f + (0.1f * hitLevel);
    }
}
