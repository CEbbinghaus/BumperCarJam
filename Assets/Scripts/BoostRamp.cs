using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRamp : MonoBehaviour
{
    public float speedMultiplier = 2.0f;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity += transform.forward * (speedMultiplier * Time.deltaTime);
        }
    }
}
