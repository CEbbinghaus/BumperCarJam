using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.position = collision.transform.GetComponent<Player>().spawnPoint;
            collision.transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }
}
