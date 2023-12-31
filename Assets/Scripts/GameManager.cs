using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int numberOfBurnableObjects;
    [SerializeField]
    int numberOfIgniters;
    [SerializeField]
    GameObject pauseOverlay;
    [SerializeField]
    GameObject loadingOverlay;
    [SerializeField]
    GameObject menuBar;
    [SerializeField]
    InputField numberOfBurnableObjectsInput;
    [SerializeField]
    InputField numberOfIgnitersInput;
    [SerializeField]
    ScoreManager scoreManager;
    Terrain terrain;
    SimulationModeManager simulationModeManager;
    private void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        simulationModeManager = GetComponent<SimulationModeManager>();
        if (simulationModeManager.mode == SimulationMode.Automatic)
        {
            numberOfBurnableObjects = UnityEngine.Random.Range(1, numberOfBurnableObjects);
            numberOfIgniters = UnityEngine.Random.Range(1, numberOfIgniters);
            SpawnBurnableObject();
        } else
        {
            SetNumberOfBurnableObject();
            SetNumberOfIgniters();
        }
    }
    public void IgniteRandomBurnableObject()
    {
        GameObject[] burnableObjects;
        burnableObjects = GameObject.FindGameObjectsWithTag("BurnableObject");
        if (burnableObjects.Length > 0)
        {
            for (int i = 0; i < numberOfIgniters; i++)
            {
                GameObject burnableObject = burnableObjects[UnityEngine.Random.Range(0, burnableObjects.Length)];
                burnableObject.GetComponent<BurnableObject>().SetBurning();
                if (scoreManager && i == 0) scoreManager.isGameStarted = true;
            }
        }
    }
    public void SpawnIgniter(Vector3 position)
    {
        GameObject igniter = ObjectPool.SharedInstance.GetIgniterPooled();
        if (igniter != null)
        {
            igniter.transform.position = position;
            igniter.SetActive(true);
        }
    }
    public void SpawnBurnableObject()
    {
        loadingOverlay.SetActive(true);
        menuBar.SetActive(false);
        ClearBurnableObjects();
        StartCoroutine("SpawnBurnableObjectsCoroutine");
    }
    Vector3 GenerateTerrainPosition(float yOffset)
    {
        //Get terrain size
        float width = terrain.terrainData.size.x;
        float length = terrain.terrainData.size.z;

        //Get terrain position
        float x = terrain.transform.position.x;
        float z = terrain.transform.position.z;

        // Get random position
        float randX = UnityEngine.Random.Range(x, x + width);
        float randZ = UnityEngine.Random.Range(z, z + length);
        float yVal = terrain.SampleHeight(new Vector3(randX, 0, randZ)) + yOffset;

        return new Vector3(randX, yVal, randZ);
    }
    public void ClearBurnableObjects()
    {
        GameObject[] bos = GameObject.FindGameObjectsWithTag("BurnableObject");
        for (int i = 0; i < bos.Length; i++)
        {
            BurnableObject burnableObject = bos[i].GetComponent<BurnableObject>();
            burnableObject.ResetBurnableObject();
        }
    }
    public void ToggleSimulation()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseOverlay.SetActive(!pauseOverlay.activeInHierarchy);
    }
    public void SetNumberOfBurnableObject()
    {
        if (numberOfBurnableObjectsInput)
        {
            numberOfBurnableObjects = Convert.ToInt32(numberOfBurnableObjectsInput.text);
        }
    }
    public void SetNumberOfIgniters()
    {
        if(numberOfIgnitersInput)
        {
            numberOfIgniters = Convert.ToInt32(numberOfIgnitersInput.text);
        }
    }
    // TODO: Loading screen only pops if the for loop is delayed, need to address this issue
    IEnumerator SpawnBurnableObjectsCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        for (int i = 0; i < numberOfBurnableObjects; i++)
        {
            GameObject burnableObject = ObjectPool.SharedInstance.GetBurnableObjectPooled();
            if (burnableObject != null)
            {
                burnableObject.transform.position = GenerateTerrainPosition(0);
                burnableObject.SetActive(true);
            }
        }
        loadingOverlay.SetActive(false);
        menuBar.SetActive(true);
        if(simulationModeManager.mode == SimulationMode.Automatic)
        {
            StartCoroutine("IgniteWithDelay");
        }
    }
    IEnumerator IgniteWithDelay()
    {
        yield return new WaitForSeconds(3);
        IgniteRandomBurnableObject();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
