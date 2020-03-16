using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject LeftPlatform;
    public GameObject RightPlatform;

    public bool isMovingRight = true;
    public float movementSpeed = 1.0f;

    private Vector3 firstPos;
    private Vector3 movementOffset;
    private float movement;
    void Update()
    {
        Vector3 lPosition = transform.localPosition;
        firstPos = transform.position;
        movement = Time.deltaTime * movementSpeed;

        if (isMovingRight)
            transform.localPosition = new Vector3(lPosition.x += movement, lPosition.y, lPosition.z);
        else
            transform.localPosition = new Vector3(lPosition.x -= movement, lPosition.y, lPosition.z);

        movementOffset = (transform.position - firstPos).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == LeftPlatform)
            isMovingRight = true;

        if (collision.gameObject == RightPlatform)
            isMovingRight = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Has to be recalculated else player goes VROOOOOOOOOMMMMM!
            movement = Time.deltaTime * movementSpeed;
            collision.transform.position += movementOffset * movement;
        }
    }
}
