using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea: MonoBehaviour
{

    public float radius = 5;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
