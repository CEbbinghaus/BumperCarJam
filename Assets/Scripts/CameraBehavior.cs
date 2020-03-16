using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public List<Transform> playerPositions;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;
    public float minZoom = 2.0f;
    public float maxZoom = 40.0f;
    public float zoomLimiter = 50.0f;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void LateUpdate()
    {
        if (playerPositions.Count == 0)
            return;
        
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position,newPosition,ref velocity, smoothTime);
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(playerPositions[0].position, Vector3.zero);
        for (int i = 0; i < playerPositions.Count; i++)
        {
            bounds.Encapsulate(playerPositions[i].position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (playerPositions.Count == 1)
        {
            return playerPositions[0].transform.position;
        }

        var bounds = new Bounds(playerPositions[0].position, Vector3.zero);
        for (int i = 0; i < playerPositions.Count; i++)
        {
            bounds.Encapsulate(playerPositions[i].position);
        }

        return bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
