using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float panSpeed = 100f;
    [SerializeField]
    float scrollSpeed = 1000f;
    Vector2 panXLimit;
    Vector2 panYLimit;
    Vector2 panZLimit;
    private void Start()
    {
        Terrain terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        float x = terrain.transform.position.x;
        float y = terrain.terrainData.bounds.max.y;
        float z = terrain.transform.position.z;
        panXLimit = new Vector2(x, x + terrain.terrainData.size.x);
        panYLimit = new Vector2(y, 150); // Max camera is 150 height, maps should not have more than, say 100
        panZLimit = new Vector2(z, z + terrain.terrainData.size.z);
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        pos.z += panSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        pos.x += panSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        pos.y -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, panXLimit.x, panXLimit.y);
        pos.y = Mathf.Clamp(pos.y, panYLimit.x, panYLimit.y);
        pos.z = Mathf.Clamp(pos.z, panZLimit.x, panZLimit.y);

        transform.position = pos;
    }
}
