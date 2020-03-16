using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float padStrength = 500.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, padStrength, 0));
    }
}
