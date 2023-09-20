using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaController : MonoBehaviour
{
    Collider spawnAreaCollider;
    Terrain terrain;
    Camera cam;
    private void Start()
    {
        spawnAreaCollider = gameObject.GetComponent<Collider>();
        cam = Camera.main;
        terrain = Terrain.activeTerrain;
    }
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (spawnAreaCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject extinguisher = ObjectPool.SharedInstance.GetExtinguisherPooled();
            float y = terrain.SampleHeight(new Vector3(hit.point.x, 0, hit.point.z)) + (extinguisher.GetComponent<Renderer>().bounds.size.y / 2);
            extinguisher.transform.position = new Vector3(hit.point.x, y, hit.point.z);
            extinguisher.SetActive(true);
        }
    }
}
