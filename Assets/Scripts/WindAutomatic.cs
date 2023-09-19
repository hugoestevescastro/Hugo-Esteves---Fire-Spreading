using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAutomatic : MonoBehaviour
{
    public float windSpeed = 100.0f;
    public float windAngle = 0;
    public Vector3 windDirection;
    GameObject windOrigin;
    [SerializeField]
    GameObject compass;
    private void Start()
    {
        windOrigin = GameObject.Find("Wind Origin").gameObject;
        windSpeed = UnityEngine.Random.Range(0, 400);
        windAngle = UnityEngine.Random.Range(0, 360);
        windDirection = CalculateWindDirection();
        compass.GetComponent<CompassManager>().SetRotation(windAngle);
    }
    // Calculated wind direction using the center of the wind box and the wind origin as references
    private Vector3 CalculateWindDirection()
    {
        return (transform.position - windOrigin.transform.position).normalized;
    }
    // While the igniter is inside the wind box, move it
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Igniter"))
        {
            other.GetComponent<Rigidbody>().AddForce(windDirection * windSpeed, ForceMode.Acceleration);
        }
    }
    // When the igniter leaves the box, destroy it
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Igniter"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
