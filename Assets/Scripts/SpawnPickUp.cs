using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUp : MonoBehaviour
{
    public GameObject pickupPrefab;

    public float respawnDelay = 2.0f;
    float respawnTimer = 0.0f;

    GameObject pickupObject;
    PickUp pickupScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(pickupPrefab);
        pickupObject = go;
        pickupScript = go.GetComponent<PickUp>();
        go.transform.parent = this.transform;

        RelocatePickUp();
    }

    void RelocatePickUp()
    {
        RaycastHit hit = new RaycastHit();

        Vector3 down = pickupObject.transform.TransformDirection(-Vector3.up);
        Vector3 spawnCenter = transform.position;
        Vector3 spawnRadius = this.transform.lossyScale * 4;
        pickupObject.transform.position = new Vector3(spawnCenter.x + Random.Range(-spawnRadius.x, spawnRadius.x), spawnCenter.y + 1, spawnCenter.z + Random.Range(-spawnRadius.z, spawnRadius.z));
        Physics.Raycast(pickupObject.transform.position, down, out hit, 200);

        while (hit.collider.gameObject != this.gameObject)
        {
            pickupObject.transform.position = new Vector3(spawnCenter.x + Random.Range(-spawnRadius.x, spawnRadius.x), spawnCenter.y + 1, spawnCenter.z + Random.Range(-spawnRadius.z, spawnRadius.z));

            Physics.Raycast(pickupObject.transform.position, down, out hit, 200);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RelocatePickUp();
        }
        
        if (Input.GetKey(KeyCode.K))
        {
            pickupScript.Kill();
        }

        if (!pickupScript.isAlive && respawnTimer > respawnDelay)
        {
            RelocatePickUp();
            pickupScript.Spawn();
            respawnTimer = 0.0f;
        }
        else if (!pickupScript.isAlive)
        {
            respawnTimer += Time.deltaTime;
        }

    }
}
