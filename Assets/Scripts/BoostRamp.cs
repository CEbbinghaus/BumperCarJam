using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRamp : MonoBehaviour
{
    public float speedMultiplier = 2.0f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject go = collision.gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.velocity += transform.forward * (speedMultiplier * 10);
        }
    }
    //

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Vector3 forward = (transform.forward * 10) + transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, forward);
    }
}
