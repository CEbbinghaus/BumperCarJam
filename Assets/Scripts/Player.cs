using UnityEngine;

public class Player : MonoBehaviour{
    
    public float Health = 0;
    public float MaxHealth = 100;
    public float HealthPercent {
        get{
            return Health / MaxHealth;
        }
    }


    public float Boost = 0;
    public float MaxBoost = 500;
    public float BoostRegen = 50;
    public float BoostUsage = 250;
    public float BoostPercentage
    {
        get
        {
            return Boost / MaxBoost;
        }
    }

    private Vector3 velocityBeforePhysicsUpdate;
    private Rigidbody rigidbody;

    public bool CanMove = false;

    bool collided = false;
    public Transform Spawn;

    BetterBumperCar controller;

    public void AddBoost(float amount)
    {
        Boost = Mathf.Clamp(Boost + amount, 0, MaxBoost);
    }

    public void Reset()
    {
        controller.wheel.transform.localRotation = Quaternion.Euler(Vector3.up * (180));
        transform.position = Spawn.position;
        transform.rotation = Spawn.rotation;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Boost = 0;
        Health = MaxHealth;
    }

    public void Awake()
    {
        Health = MaxHealth;
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<BetterBumperCar>();
        if (controller == null) throw new System.Exception("Could not find Controller Script");
        if (rigidbody == null)
            rigidbody = gameObject.AddComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rigidbody.velocity;
    }

    void Update()
    {
        AddBoost(BoostRegen * Time.deltaTime);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") collided = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collided) return;

        Player other = collision.collider.gameObject.GetComponent<Player>();
        if (other == null)
            return;


        Rigidbody rb = GetComponent<Rigidbody>();
        Rigidbody otherRB = other.GetComponent<Rigidbody>();

        Vector3 direction = (transform.position - other.transform.position).normalized;

        Vector3 velocity = other.velocityBeforePhysicsUpdate;
        float magnitude = velocity.magnitude;

        float alignment = Mathf.Clamp(Vector3.Dot(other.transform.forward, (velocityBeforePhysicsUpdate - velocity).normalized), 0.1f, 1.0f);


        if (magnitude > 0)
        {
            Health -= (magnitude * alignment) / 10f;
            print(gameObject.name + " Collided with a Object with a Velocity of: " + velocity + "\nIt had a Alignment of: " + alignment);
        }

        collided = true;

        //rb.velocity += direction * velocity;
    }
}