using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SimulationMode {
    Add = 1,
    Remove = 2,
    Toggle_Fire = 3
}

public class SimulationModeManager : MonoBehaviour
{
    public SimulationMode mode = SimulationMode.Toggle_Fire;
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
    void SetButtonText()
    {
        modeButton.GetComponentInChildren<Text>().text = mode.ToString().Replace("_", " ");
    }
}
