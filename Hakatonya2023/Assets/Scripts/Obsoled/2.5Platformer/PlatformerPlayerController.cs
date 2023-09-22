using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController: MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpheight = 1;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody rBody;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        direction = Input.GetAxis("Horizontal");
        if (direction > 0) {
            rBody.velocity = new Vector3(direction * speed, rBody.velocity.y, 0);
        } else if (direction < 0) {
            rBody.velocity = new Vector3(direction * speed, rBody.velocity.y, 0);
        } else {
            rBody.velocity = new Vector3(0, rBody.velocity.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            rBody.velocity = new Vector3(rBody.velocity.x, jumpheight, 0);
        }

    }

}
