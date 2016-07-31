using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour
{
    private float rotationSpeed = 0.1f;

	private void Start()
	{
        StartCoroutine(Spin());
	}

    private IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed * 360);
            yield return 0;
        }
    }
}
