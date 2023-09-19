using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SimulationMode {
    Add = 1,
    Remove = 2,
    Toggle_Fire = 3,
    Automatic = 4,
}

public class SimulationModeManager : MonoBehaviour
{
    public SimulationMode mode;
    [SerializeField]
    Button modeButton;
    void Start()
    {
        mode = SimulationMode.Toggle_Fire;
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
                mode = SimulationMode.Toggle_Fire;
                break;
            case SimulationMode.Toggle_Fire:
                mode = SimulationMode.Add;
                break;
            default:
                mode = SimulationMode.Add;
                break;
        }
        SetButtonText();
    }
    public void SetMode(SimulationMode md)
    {
        mode = md;
    }
    void SetButtonText()
    {
       if (modeButton)
        {
            modeButton.GetComponentInChildren<Text>().text = mode.ToString().Replace("_", " ");
        }
    }
}
