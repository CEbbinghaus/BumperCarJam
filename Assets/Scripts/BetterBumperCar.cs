using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBumperCar : MonoBehaviour{
    Rigidbody rb;

    float Direction = 0;
    float rotation = 0;
    
    public float WheelRotationSpeed = 50.0f;

    public float Speed = 100.0f;
    public float RotaionalSpeed = 200.0f;

    public float BoostMultiplier = 2.5f;

    Vector3 velocity;

    public float Resistance = .1f;

    public Vector2 WheelOffset;

    public Mesh m;

    public GameObject wheel;

    public string VerticalInputAxis = "Vertical";
    public string HorizontalInputAxis = "Horizontal";
    public KeyCode BoostKey = KeyCode.LeftShift;


    private Player playerComponent;
    void Start()
    {
        if(wheel == null)
            wheel = new GameObject("Wheel");

        playerComponent = GetComponent<Player>();
        if (playerComponent == null) throw new 
                System.Exception("Could Not Find Player Component on Player");

        Vector2 forward =  new Vector2(transform.forward.x, transform.forward.z);
        float forwardAngle = VectorToAngle(forward);
        Vector2 finalOffset = rotate(WheelOffset, Mathf.Deg2Rad * forwardAngle);
        
        wheel.transform.position = transform.position + new Vector3(finalOffset.x, 1f, finalOffset.y);
        wheel.transform.localRotation = Quaternion.Euler(0, 180, 0);
        wheel.transform.parent = this.transform; 
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        if (!playerComponent.CanMove) return;
        
        velocity = rb.velocity;

        float direction = Input.GetAxis(HorizontalInputAxis);
        
        float wheelRotataion = wheel.transform.localEulerAngles.y + (direction * WheelRotationSpeed * Time.deltaTime);

        wheelRotataion = Mathf.Clamp(wheelRotataion, 90, 270);

        //print(wheelRotataion);

        wheel.transform.localRotation = Quaternion.Euler(Vector3.up * (wheelRotataion));
        

        Direction = wheel.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        //rotation = transform.rotation.eulerAngles.y;

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

        float VInput = Input.GetAxis(VerticalInputAxis);

        float speed = VInput * Speed;

        //Default Amount. Will
        float boostAmount = 1.0f;

        if(Input.GetKey(BoostKey) && playerComponent.Boost > 0)
        {
            boostAmount = BoostMultiplier;
            playerComponent.Boost -= Time.deltaTime * playerComponent.BoostUsage;
        }

        Vector2 speedDir = (Dir * forwardAmount) * speed * boostAmount * Time.deltaTime;
        

        velocity += new Vector3(speedDir.x, 0, speedDir.y);

        //velocity.x *= (1 - Resistance);
        //velocity.z *= (1 - Resistance);

        // rotation = rotation + (rightAmount * ((RotaionalSpeed * VInput)) * Time.deltaTime);

        rb.angularVelocity += new Vector3(0, (rightAmount * boostAmount * ((RotaionalSpeed * VInput))) * Time.deltaTime, 0);

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
