using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isAlive = true;

    public float boostAmount = 0.5f;

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
            OldPlayer player = collision.transform.GetComponent<OldPlayer>();

            if (player.BoostAmount + boostAmount > 1.0f)
                player.BoostAmount = 1.0f;
            else
               player.BoostAmount += boostAmount;

        }
    }

}
