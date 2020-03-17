using UnityEngine;

public class Player : MonoBehaviour{
    
    [ReadOnly]public float Health;
    public float MaxHealth = 100;
    public float HealthPercent {
        get{
            return MaxHealth / Health;
        }
    }

    private Vector3 velocityBeforePhysicsUpdate;
    private Rigidbody rigidbody;

    public void Awake()
    {
        Health = MaxHealth;
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            rigidbody = gameObject.AddComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rigidbody.velocity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Player other = collision.collider.gameObject.GetComponent<Player>();
        if (other == null)
            return;

        Rigidbody rb = GetComponent<Rigidbody>();
        Rigidbody otherRB = other.GetComponent<Rigidbody>();

        Vector3 direction = (transform.position - other.transform.position).normalized;

        float velocity = other.velocityBeforePhysicsUpdate.magnitude;
        if (velocity > 0)
        {
            Health -= Mathf.Pow(velocity, 0.8f);
            print(gameObject.name + " Collided with a Object with a Velocity of: " + velocity);
        }


        //rb.velocity += direction * velocity;
    }
}