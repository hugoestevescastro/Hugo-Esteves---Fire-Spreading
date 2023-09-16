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
    GameObject pauseOverlay;
    [SerializeField]
    InputField numberOfBurnableObjectsInput;
    [SerializeField]
    InputField numberOfIgnitersInput;
    float burnableObjectHeight;
    Terrain terrain;
    private void Start()
    {
        burnableObjectHeight = -1;
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
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
        GameObject igniter = ObjectPool.SharedInstance.GetIgniterPooled();
        if (igniter != null)
        {
            //igniter.transform.SetPositionAndRotation(position, Quaternion.identity);
            igniter.transform.position = position;
            igniter.SetActive(true);
        }
    }
    public void SpawnBurnableObject()
    {
        ClearBurnableObjects();
        for (int i = 0; i < numberOfBurnableObjects; i++)
        {
            GameObject burnableObject = ObjectPool.SharedInstance.GetBurnableObjectPooled();
            if (burnableObject != null)
            {
                if (burnableObjectHeight == -1) burnableObjectHeight = burnableObject.GetComponent<Renderer>().bounds.size.y;
                burnableObject.transform.position = GenerateTerrainPosition(burnableObjectHeight / 2); //SetPositionAndRotation(GenerateTerrainPosition(burnableObject.GetComponent<Renderer>().bounds.size.y / 2), Quaternion.identity);
                burnableObject.SetActive(true);
            }
        }
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
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("BurnableObject"))
        {
            BurnableObject burnableObject = gameObj.GetComponent<BurnableObject>();
            burnableObject.SetNotBurning();
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
