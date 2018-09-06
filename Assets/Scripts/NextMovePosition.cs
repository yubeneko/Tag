using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMovePosition : MonoBehaviour {

	float radius = 0.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
