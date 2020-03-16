using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCar : MonoBehaviour{
    Rigidbody rb;

    float Direction;
    
    public float Speed = 10.0f;

    Vector3 velocity;

    public float Resistance = .1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        
        velocity = rb.velocity;

        float direction = Input.GetAxis("Horizontal");

        Direction += direction * 0.05f;

        //Direction = Direction;

        Vector2 dirVector = new Vector2(Mathf.Sin(Direction), Mathf.Cos(Direction));

        float speed = Input.GetAxis("Vertical") * Speed;

        Vector2 speedDir = (dirVector * speed) * Time.deltaTime;

        print(speedDir);

        velocity *= (1 - Resistance);

        float rotation = 360f - ((Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg + 360) % 360);

        rb.rotation = Quaternion.Euler(0, rotation, 0);

        rb.velocity = new Vector3(speedDir.x, rb.velocity.y, speedDir.y);
    }
}
