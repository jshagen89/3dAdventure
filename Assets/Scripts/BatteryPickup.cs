using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour
{
    private float rotationSpeed = 0.1f;
    private float floatSpeed = 0.4f;
    private float maxMovementDistance = 0.3f;
    private bool isMovingUp = true;
    private float startingY;
    private float newY;

    private void Awake()
    {
        startingY = transform.position.y;
    }

    private void Update()
    {
        Spin();
        Float();
    }

    private void Spin()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed * 360);
    }

    private void Float()
    {
        newY = transform.position.y + (isMovingUp ? 1 : -1) * 2 * maxMovementDistance * floatSpeed * Time.deltaTime;
        if (newY >= startingY + maxMovementDistance)
        {
            newY = startingY + maxMovementDistance;
            isMovingUp = false;
        }
        else if (newY < startingY)
        {
            newY = startingY;
            isMovingUp = true;
        }
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
