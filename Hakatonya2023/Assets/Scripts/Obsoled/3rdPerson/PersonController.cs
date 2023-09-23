using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController: MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpheight = 1;
    public float sensetivity = 1;
    public GameObject camTarget;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;

    public Vector3 direction;

    private bool inAir = false;
    private Vector2 mouseInput;

    // Start is called before the first frame update
    void Start() {
        rBody = GetComponent<Rigidbody>();
        mouseInput = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update() {

        direction = transform.forward;
        
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        mouseInput.x += Input.GetAxis("Mouse X") * sensetivity;
        mouseInput.y += Input.GetAxis("Mouse Y") * sensetivity;

        if (!inAir) {
            if (Input.GetButtonDown("Jump")) {
                rBody.velocity = new Vector3(rBody.velocity.x, jumpheight, rBody.velocity.z);
                inAir = true;
            }
        }

        if (!inAir) {
            rBody.velocity = ((transform.right * horizontal) + (transform.forward * vertical)) * speed;
        }

    

    }

    private void LateUpdate() {
        transform.localRotation = Quaternion.Euler(0, mouseInput.x, 0);
        camTarget.transform.localRotation = Quaternion.Euler(-mouseInput.y, 0, 0);

        //rBody.velocity = new Vector3(direction.x * speed, direction.y, direction.z * speed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.tag == "Floor") {
            inAir = false;
        }
    }
}
