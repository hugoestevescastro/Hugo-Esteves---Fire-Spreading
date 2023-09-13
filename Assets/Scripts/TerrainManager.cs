using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    SimulationModeManager simulationModeManager;
    private void Start()
    {
        simulationModeManager = GameObject.Find("Game Manager").GetComponent<SimulationModeManager>();
    }
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0) || simulationModeManager.mode != SimulationMode.Add) return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(gameObject.GetComponent<TerrainCollider>().Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject burnableObject = ObjectPool.SharedInstance.GetBurnableObjectPooled();
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(hit.point.x, 0, hit.point.z)) + (burnableObject.GetComponent<Renderer>().bounds.size.y / 2);
            burnableObject.transform.position = new Vector3(hit.point.x, y, hit.point.z);
            burnableObject.transform.rotation = Quaternion.identity;
            burnableObject.SetActive(true);
        }
    }
}
