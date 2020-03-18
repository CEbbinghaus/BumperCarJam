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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){

            Player player = collision.transform.GetComponent<Player>();
            if (player.Boost >= player.MaxBoost) return;
            Kill();
            
            player.AddBoost(boostAmount);

        }
    }

}
