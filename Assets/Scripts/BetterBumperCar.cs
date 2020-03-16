using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBumperCar : MonoBehaviour{
    Rigidbody rb;

    float Direction = 3.1415f;
    float rotation = 0;
    
    public float Speed = 10.0f;

    Vector3 velocity;

    public float Resistance = .1f;

    public Vector2 WheelOffset;

    public Mesh m;

    GameObject wheel;

    void Start()
    {
        wheel = new GameObject("Wheel");

        Vector2 forward =  new Vector2(transform.forward.x, transform.forward.z);
        float forwardAngle = VectorToAngle(forward);
        Vector2 finalOffset = rotate(WheelOffset, Mathf.Deg2Rad * forwardAngle);
        
        wheel.transform.position = transform.position + new Vector3(finalOffset.x, 0, finalOffset.y);
        wheel.transform.parent = this.transform; 
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        
        velocity = rb.velocity;

        float direction = Input.GetAxis("Horizontal");

        wheel.transform.rotation *= Quaternion.Euler(0,  direction * 5, 0);
        Direction = wheel.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        //Direction = Direction;

        Vector2 dirVector = new Vector2(Mathf.Sin(Direction), Mathf.Cos(Direction));

        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2 forward =  new Vector2(transform.forward.x, transform.forward.z);
        Vector2 right =  new Vector2(transform.right.x, transform.right.z);

        float forwardAngle = VectorToAngle(forward);
        Vector2 tmp = rotate(WheelOffset, Mathf.Deg2Rad * forwardAngle);

        Vector2 offset = position + tmp;

        Vector2 offsetDir = offset + dirVector;

        Vector2 Dir = (offsetDir - position).normalized;

        float forwardAmount = Vector2.Dot(dirVector.normalized, -forward.normalized);
        float rightAmount = Vector2.Dot(dirVector.normalized, -right.normalized);

        //print(rightAmount);

        Debug.DrawRay(new Vector3(offset.x, 0, offset.y), new Vector3(dirVector.x, 0, dirVector.y), Color.red);
        Debug.DrawRay(new Vector3(position.x, 0, position.y), new Vector3(Dir.x, 0, Dir.y), Color.blue);

        float speed = Input.GetAxis("Vertical") * Speed;

        Vector2 speedDir = (Dir * forwardAmount) * speed * Time.deltaTime;

        // if(forwardAmount < 0)
        //     speedDir *= Vector2.right * -1;

        velocity += new Vector3(speedDir.x, 0, speedDir.y);
        //print(velocity);

        //print(velocity);

        velocity *= (1 - Resistance);

        float next = VectorToAngle(Dir) * Mathf.Deg2Rad;

        //print();
        //wheel.transform.rotation = Quaternion.Euler(0, Direction * Mathf.Rad2Deg, 0);

        float diff = (next - rotation) / 10;

        rotation += diff * (rightAmount * speed * Time.deltaTime);

        rb.rotation = Quaternion.Euler(0, rotation, 0);

        rb.velocity = velocity;
    }

    void OnDrawGizmos(){
        // Draw a yellow sphere at the transform's position
        Vector2 forward =  new Vector2(transform.forward.x, transform.forward.z);
        
        //print(forward);

        float forwardAngle = VectorToAngle(forward);
        Vector2 finalOffset = rotate(WheelOffset, Mathf.Deg2Rad * forwardAngle);

        //print(forwardAngle);


        Gizmos.color = Color.white;
        Gizmos.DrawWireMesh(m, transform.position + new Vector3(finalOffset.x, 0, finalOffset.y), Quaternion.Euler(0, Mathf.Rad2Deg * Direction, 0), Vector3.one * 0.4f);
    }

    Vector2 rotate(Vector2 position, float amount){
        Vector2 res;
        res.x = position.x * Mathf.Cos(amount) - position.y * Mathf.Sin(amount);
        res.y = position.x * Mathf.Sin(amount) + position.y * Mathf.Cos(amount);
        return res;
    }

    float VectorToAngle(Vector2 vec){
        return 270f - ((Mathf.Atan2(-vec.y, vec.x) * Mathf.Rad2Deg + 360) % 360);
    }

}
