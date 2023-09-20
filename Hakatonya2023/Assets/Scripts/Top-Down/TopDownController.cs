using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpheight = 1;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float horizontal;
    private float vertical;
    private bool inAir = false;

    // Start is called before the first frame update
    void Start() {
        rBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (!inAir) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rBody.velocity = new Vector3(rBody.velocity.x, jumpheight, rBody.velocity.z);
                inAir = true;
            }
        } else {
            vertical /= 2;
            horizontal /= 2;
        }


        if (!inAir) {
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

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.tag == "Floor") {
            inAir = false;
        }
    }
}
