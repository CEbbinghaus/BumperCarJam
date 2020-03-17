using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float padStrength = 500.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 up = transform.up;
            up *= padStrength;
            collision.gameObject.GetComponent<Rigidbody>().velocity += up;
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(up);
        
        }
    }
}
