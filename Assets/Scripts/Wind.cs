using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wind : MonoBehaviour
{
    public float windSpeed = 100.0f;
    public float windAngle = 0;
    public Vector3 windDirection;
    public Slider windSpeedSlider;
    public Slider windDirectionSlider;
    public Text windSpeedText;
    public Text windDirectionText;
    GameObject windOrigin;
    private void Start()
    {
        windOrigin = GameObject.Find("Wind Origin").gameObject;
        SetDirectionSliderValue();
    }
    public void SetSpeedSliderValue()
    {
        windSpeed = windSpeedSlider.value;
        windSpeedText.text = Convert.ToString(windSpeed);
    }
    public void SetDirectionSliderValue()
    {
        // Set the Y angle value
        windAngle = windDirectionSlider.value;

        // Rotate the wind box
        Utilities.RotateObject(GameObject.Find("Wind Box"), new Vector3(0, windAngle, 0));

        // Set the new direction
        windDirection = CalculateWindDirection();

        // Set the angle value
        windDirectionText.text = Convert.ToString(windAngle) + "º";
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
