using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{

    public float speed = 1 ;
    public float maxSpeed;
    public bool rush = false;
    public float endurance = 1;
    public bool isHidden = false;
    public float life = 1;
    public float radiation = 0;
    public float food = 1;
    public float weight = 1;
    public float growLevel = 0.01f;
    public float level = 1;

    public float hungerSpeed = 0.01f;
    public float hungerDeathSpeed = 0.01f;

    public Animator animController;

    public float interactDistance = 2;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;




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

        if (food < 0) {
            life -= hungerDeathSpeed * Time.deltaTime;
        }

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");



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
