using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int numberOfBurnableObjects;
    [SerializeField]
    int numberOfIgniters;
    [SerializeField]
    GameObject igniter;
    [SerializeField]
    GameObject pauseOverlay;
    [SerializeField]
    InputField numberOfBurnableObjectsInput;
    [SerializeField]
    InputField numberOfIgnitersInput;
    private void Start()
    {
        SetNumberOfBurnableObject();
        SetNumberOfIgniters();
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
            }
        }
    }
    public void SpawnIgniter(Vector3 position)
    {
        Instantiate(igniter, position, Quaternion.identity);
    }
    public void SpawnBurnableObject()
    {
        ClearBurnableObjects();
        for (int i = 0; i < numberOfBurnableObjects; i++)
        {
            GameObject burnableObject = ObjectPool.SharedInstance.GetPooledObject();
            if (burnableObject != null)
            {
                burnableObject.transform.position = GenerateTerrainPosition(burnableObject.GetComponent<Renderer>().bounds.size.y / 2);
                burnableObject.transform.rotation = Quaternion.identity;
                burnableObject.SetActive(true);
            }
        }
    }
    Vector3 GenerateTerrainPosition(float yOffset)
    {
        Terrain terrain = GameObject.Find("Terrain").GetComponent<Terrain>();

        //Get terrain size
        float width = terrain.terrainData.size.x;
        float length = terrain.terrainData.size.z;

        //Get terrain position
        float x = terrain.transform.position.x;
        float z = terrain.transform.position.z;

        // Get random position
        float randX = UnityEngine.Random.Range(x, x + width);
        float randZ = UnityEngine.Random.Range(z, z + length);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)) + yOffset;

        return new Vector3(randX, yVal, randZ);
    }
    public void ClearBurnableObjects()
    {
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("BurnableObject"))
        {
            gameObj.SetActive(false);
        }
    }
    public void ToggleSimulation()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseOverlay.SetActive(!pauseOverlay.activeInHierarchy);
    }
    public void SetNumberOfBurnableObject()
    {
        numberOfBurnableObjects = Convert.ToInt32(numberOfBurnableObjectsInput.text);
    }
    public void SetNumberOfIgniters()
    {
        numberOfIgniters = Convert.ToInt32(numberOfIgnitersInput.text);
    }
}
