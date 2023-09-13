using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    // Update is called once per frame
    public Slider windDirectionSlider;
    public void OnDirectionSliderChange()
    {
        Utilities.RotateObject(gameObject, new Vector3(0, 0, -windDirectionSlider.value));
    }
}
