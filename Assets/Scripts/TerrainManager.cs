using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    SimulationModeManager simulationModeManager;
    TerrainCollider terrainCollider;
    Camera cam;
    Terrain terrain;
    private void Start()
    {
        simulationModeManager = GameObject.Find("Game Manager").GetComponent<SimulationModeManager>();
        terrainCollider = gameObject.GetComponent<TerrainCollider>();
        cam = Camera.main;
        terrain = Terrain.activeTerrain;
    }
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0) || simulationModeManager.mode != SimulationMode.Add) return;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(terrainCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject burnableObject = ObjectPool.SharedInstance.GetBurnableObjectPooled();
            float y = terrain.SampleHeight(new Vector3(hit.point.x, 0, hit.point.z)) + (burnableObject.GetComponent<Renderer>().bounds.size.y / 2);
            burnableObject.transform.position = new Vector3(hit.point.x, y, hit.point.z);
            burnableObject.SetActive(true);
        }
    }
}
