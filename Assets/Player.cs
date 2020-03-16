using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BoostAmount = 0.0f;
    Rigidbody rb;
    public float boostIdleRecharge = 0.02f;
    public float movementSpeed = 2.0f;
    public int player = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 0)
        {
            float speed = movementSpeed;
            if (Input.GetKey(KeyCode.LeftShift) && BoostAmount > 0.0f)
            {
                BoostAmount -= Time.deltaTime / 2;
                speed = movementSpeed * 2;
            }
            else
                BoostAmount += Time.deltaTime * boostIdleRecharge;

            if (Input.GetKey(KeyCode.W))
                rb.AddForce(Vector3.forward * speed);

            if (Input.GetKey(KeyCode.S))
                rb.AddForce(-Vector3.forward * speed);

            if (Input.GetKey(KeyCode.D))
                rb.AddForce(Vector3.right * speed);

            if (Input.GetKey(KeyCode.A))
                rb.AddForce(-Vector3.right * speed);
        }
        else if (player == 1)
        {
            float speed = movementSpeed;
            if (Input.GetKey(KeyCode.RightShift) && BoostAmount > 0.0f)
            {
                BoostAmount -= Time.deltaTime / 2;
                speed = movementSpeed * 2;
            }
            else
                BoostAmount += Time.deltaTime * boostIdleRecharge;

            if (Input.GetKey(KeyCode.UpArrow))
                rb.AddForce(Vector3.forward * speed);

            if (Input.GetKey(KeyCode.DownArrow))
                rb.AddForce(-Vector3.forward * speed);

            if (Input.GetKey(KeyCode.RightArrow))
                rb.AddForce(Vector3.right * speed);

            if (Input.GetKey(KeyCode.LeftArrow))
                rb.AddForce(-Vector3.right * speed);

        }

        if (Input.GetKey(KeyCode.C))
        {
            BoostAmount = 0.0f;
        }
    }
}
