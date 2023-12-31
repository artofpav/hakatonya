using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Food
{
    protected GameObject target;
    public enum EnemyState {Idle, Attack, Run}

    public EnemyState state = EnemyState.Idle;
    public float speed = 1;

    public float wanderDistance = 10;
    public float awarnesDistance = 15;
    public Animator anim;
    //public Rigidbody rBody;

    protected Vector3 targetPosition;
    protected Vector3 wanderPoint;
    protected RaycastHit hit;
    // note that the ray starts at 100 units
    protected Ray ray;
    public LayerMask goundColliderMask;

    public float riseDistance = 0.2f;


    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Hero")) {

            GameManager.singl.HitHero(this);
        }
    }
    private void Start() {
        if (target == null) {
            target = new GameObject();
        }
        
    }

    private void OnEnable() {
        wanderPoint = transform.position;
    }

    private void Update() {
        if (state == EnemyState.Idle) {


            if (Vector3.Distance(transform.position, GameManager.singl.hero.transform.position) < awarnesDistance && !GameManager.singl.hero.isHidden) {
                if (Mathf.Floor(GameManager.singl.hero.level) <= level) {
                    state = EnemyState.Attack; 
                } else {
                    state = EnemyState.Run;
                }
            }


            if ((Vector3.Distance(transform.position, target.transform.position) > wanderDistance) ||
                (Vector3.Distance(transform.position, target.transform.position) < 1f)) {

                targetPosition = new Vector3(Random.insideUnitCircle.x * wanderDistance, 0, Random.insideUnitCircle.y * wanderDistance);

                target.transform.position = targetPosition + wanderPoint;

                ray = new Ray(target.transform.position + Vector3.up * 100, Vector3.down);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {

                    // this is where the gameobject is actually put on the ground
                    target.transform.position = new Vector3(target.transform.position.x, hit.point.y + riseDistance, target.transform.position.z);

                }
            } 


            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(target.transform.position-transform.position, Vector3.up), 
                360 * Time.deltaTime);

            transform.position = transform.position + (transform.forward * Time.deltaTime* speed/2);


            ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {
                if (hit.collider != null & Vector3.Distance(hit.point, transform.position) > riseDistance) {
                    // this is where the gameobject is actually put on the ground
                    transform.position = new Vector3(transform.position.x, hit.point.y + riseDistance, transform.position.z);
                }
            }

        } else if (state == EnemyState.Attack) {
            if (GameManager.singl.hero.isHidden || Vector3.Distance(transform.position, GameManager.singl.hero.transform.position) > awarnesDistance+5) {
                state = EnemyState.Idle;
            }

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(GameManager.singl.hero.transform.position - transform.position, Vector3.up),
                360 * Time.deltaTime);

            transform.position = transform.position + (transform.forward * Time.deltaTime * speed);


            ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {
                if (hit.collider != null & Vector3.Distance(hit.point, transform.position) > riseDistance) {
                    // this is where the gameobject is actually put on the ground
                    transform.position = new Vector3(transform.position.x, hit.point.y + riseDistance, transform.position.z);
                }
            }
        } else if (state == EnemyState.Run) {
            if (GameManager.singl.hero.isHidden || Vector3.Distance(transform.position, GameManager.singl.hero.transform.position) > awarnesDistance + 5) {
                state = EnemyState.Idle;
            }

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(transform.position - GameManager.singl.hero.transform.position, Vector3.up),
                360 * Time.deltaTime);

            transform.position = transform.position + (transform.forward * Time.deltaTime * speed);


            ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, goundColliderMask)) {
                if (hit.collider != null & Vector3.Distance(hit.point, transform.position) > riseDistance) {
                    // this is where the gameobject is actually put on the ground
                    transform.position = new Vector3(transform.position.x, hit.point.y + riseDistance, transform.position.z);
                }
            }

        }
    }
}
