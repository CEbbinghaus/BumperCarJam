using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    public float Speed = 10.0f;


   // Vector2 direction;

    public void Update(){
        Vector2 InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 direction = InputVector.normalized;

        float rotation = 360 - ((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360);
        print(rotation);

        transform.rotation = Quaternion.Euler(0, rotation, 0);
        //transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y));
        Vector2 finalOffset = direction * Speed * Time.deltaTime;
        transform.position += new Vector3(finalOffset.x, 0, finalOffset.y);
    }
}
