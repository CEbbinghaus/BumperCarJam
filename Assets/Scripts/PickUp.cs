using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isAlive = true;

    public float boostAmount = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void Spawn()
    {
        this.gameObject.SetActive(true);
        isAlive = true;
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
        isAlive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Kill();
            collision.gameObject.GetComponent<Player>().BoostAmount += boostAmount;
        }
    }

}
