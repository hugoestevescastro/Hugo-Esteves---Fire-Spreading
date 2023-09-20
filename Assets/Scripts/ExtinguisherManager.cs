using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguisherManager : MonoBehaviour
{
    public int maxNumberOfExtinguishers;
    public int usedNumberOfExtinguisher = 0;
    [SerializeField]
    Text extinguishersTextInput;
    void Start()
    {
        UpdateExtinguisherCounter(0);
    }

    public bool AllowExtinguisherSpawn()
    {
        return maxNumberOfExtinguishers > usedNumberOfExtinguisher;
    }

    public void UpdateExtinguisherCounter(int i)
    {
        usedNumberOfExtinguisher += i;
        if (extinguishersTextInput) extinguishersTextInput.text = (maxNumberOfExtinguishers - usedNumberOfExtinguisher).ToString();
    }
}
