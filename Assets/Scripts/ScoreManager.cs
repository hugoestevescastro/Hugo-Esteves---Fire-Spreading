using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject endgameOverlay;
    public int score;
    public bool isGameStarted;
    public bool isGameFinished;
   
    void Start()
    {
        score = 0;
        isGameStarted = false;
        isGameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameFinished && isGameStarted)
        {
            isGameFinished = IsGameFinished();
        }
    }

    bool IsGameFinished()
    {
        List<GameObject> activeBurnableObjects = ObjectPool.SharedInstance.GetActiveBurnableObject();
        for (int i = 0; i < activeBurnableObjects.Count; i++)
        {
            BurnableObject bo = activeBurnableObjects[i].GetComponent<BurnableObject>();
            if (bo.state == BurnableObjectState.Burning) return false;
        }
        score = activeBurnableObjects.Count;
        TriggerEndgame();
        return true;
    }
    public void TriggerEndgame()
    {
        // Pause everything
        Time.timeScale = 0;

        //Set score text
        if (scoreText) scoreText.text = score + " pts";

        // Open up the overlay
        endgameOverlay.SetActive(true);

    }
}
