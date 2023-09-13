using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SimulationMode {
    Add = 1,
    Remove = 2,
    ToggleFire = 3
}

public class SimulationModeManager : MonoBehaviour
{
    public SimulationMode mode = SimulationMode.ToggleFire;
    [SerializeField]
    Button modeButton;
    void Start()
    {
        SetButtonText();
    }

    /** Cycles the game mode by mouse click */
    public void CycleMode()
    {
        switch (mode)
        {
            case SimulationMode.Add:
                mode = SimulationMode.Remove;
                break;
            case SimulationMode.Remove:
                mode = SimulationMode.ToggleFire;
                break;
            case SimulationMode.ToggleFire:
                mode = SimulationMode.Add;
                break;
            default:
                mode = SimulationMode.Add;
                break;
        }
        SetButtonText();
    }
    void SetButtonText()
    {
        modeButton.GetComponentInChildren<Text>().text = mode.ToString();
    }
}
