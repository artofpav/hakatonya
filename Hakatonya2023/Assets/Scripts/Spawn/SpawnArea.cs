using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea: MonoBehaviour
{

    public float radius = 5;
    // Start is called before the first frame update
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
