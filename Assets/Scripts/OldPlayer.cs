using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayer : MonoBehaviour
{
    public float BoostAmount = 0.0f;
    Rigidbody rb;
    public float boostIdleRecharge = 0.02f;
    public float movementSpeed = 30.0f;
    public float maxPlayerMovementSpeed = 10.0f;
    public int player = 0;
    public float boostMultiplier = 2.0f;
    public Vector3 spawnPoint;
    public bool isPlayerGrounded;
    public float maxSpeed = 20.0f;
    public float healthAmount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 down = transform.TransformDirection(-Vector3.up);
        isPlayerGrounded = Physics.Raycast(transform.position, down, 1f);

        float speed = movementSpeed;

        if (!isPlayerGrounded)
            speed *= 0.25f;

        if (player == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && BoostAmount > 0.0f)
            {
                BoostAmount -= Time.deltaTime / 2;
                speed *= boostMultiplier;
            }
            else
                BoostAmount += Time.deltaTime * boostIdleRecharge;

            if (Input.GetKey(KeyCode.W) && rb.velocity.z < maxPlayerMovementSpeed)
                rb.AddForce(Vector3.forward * speed);

            if (Input.GetKey(KeyCode.S) && rb.velocity.z > -maxPlayerMovementSpeed)
                rb.AddForce(-Vector3.forward * speed);

            if (Input.GetKey(KeyCode.D) && rb.velocity.x < maxPlayerMovementSpeed)
                rb.AddForce(Vector3.right * speed);

            if (Input.GetKey(KeyCode.A) && rb.velocity.x > -maxPlayerMovementSpeed)
                rb.AddForce(-Vector3.right * speed);
        }
        else if (player == 1)
        {
            if (Input.GetKey(KeyCode.RightShift) && BoostAmount > 0.0f)
            {
                BoostAmount -= Time.deltaTime / 2;
                speed *= boostMultiplier;
            }
            else
                BoostAmount += Time.deltaTime * boostIdleRecharge;

            if (Input.GetKey(KeyCode.UpArrow) && rb.velocity.z < maxPlayerMovementSpeed)
                rb.AddForce(Vector3.forward * speed);

            if (Input.GetKey(KeyCode.DownArrow) && rb.velocity.z > -maxPlayerMovementSpeed)
                rb.AddForce(-Vector3.forward * speed);

            if (Input.GetKey(KeyCode.RightArrow) && rb.velocity.x < maxPlayerMovementSpeed)
                rb.AddForce(Vector3.right * speed);

            if (Input.GetKey(KeyCode.LeftArrow) && rb.velocity.x > -maxPlayerMovementSpeed)
                rb.AddForce(-Vector3.right * speed);

        }
        Vector3 currentVelocity = rb.velocity;

        currentVelocity.x = (currentVelocity.x > maxSpeed) ? maxSpeed : (currentVelocity.x < -maxSpeed) ? -maxSpeed : currentVelocity.x;

        currentVelocity.y = (currentVelocity.y > maxSpeed) ? maxSpeed : (currentVelocity.y < -maxSpeed) ? -maxSpeed : currentVelocity.y;

        currentVelocity.z = (currentVelocity.z > maxSpeed) ? maxSpeed : (currentVelocity.z < -maxSpeed) ? -maxSpeed : currentVelocity.z;

        rb.velocity = currentVelocity;

        if (healthAmount <= 0.0f)
        {
            transform.position = spawnPoint;
            healthAmount = 1.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody colRB = collision.gameObject.GetComponent<Rigidbody>();

            float collisionVelTotal = colRB.velocity.magnitude;
            float myVelTotal = rb.velocity.magnitude;

            if (collisionVelTotal > myVelTotal)
            {
                healthAmount -= collisionVelTotal / 100;
            }
        }
    }

}
