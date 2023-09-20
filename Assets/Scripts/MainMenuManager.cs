using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void LaunchSimulation()
    {
        SceneManager.LoadScene("Simulation", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
