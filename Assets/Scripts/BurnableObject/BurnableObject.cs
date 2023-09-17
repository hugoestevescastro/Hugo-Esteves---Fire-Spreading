using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BurnableObject : MonoBehaviour
{
    BurnableObjectState state = BurnableObjectState.NotBurning;
    Renderer objectRenderer;
    Collider objectCollider;
    float burningMaxDuration = 10.0f;
    GameManager gameManager;
    SimulationModeManager simulationModeManager;
    [SerializeField]
    Material[] materials;
    
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        simulationModeManager = gameManager.GetComponent<SimulationModeManager>();
        SetNotBurning();
    }

    /**
     * Sets the status to not burning and changes it to the green color
     */
    public void SetNotBurning()
    {
        state = BurnableObjectState.NotBurning;
        objectCollider.enabled = true;
        objectRenderer.material = materials[0];
    }
    /**
     * Sets the status to burning, changes the color to red, and triggers countdown to next status which is Burnt
     */
    public void SetBurning()
    {
        state = BurnableObjectState.Burning;
        objectRenderer.material = materials[1];
        // Trigger countdown for next state of burnt
        StartCoroutine("Burning");
    }
    /**
     * Sets status to burnt and changes color to black
     */
    public void SetBurnt()
    {
        state = BurnableObjectState.Burnt;
        objectCollider.enabled = false;
        objectRenderer.material = materials[2];
    }
    /*
     * After the object starts burning, triggers countdown to be burn
     */
    IEnumerator Burning()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, burningMaxDuration));
        // After it burned, spawn 3 igniters to make the fire spread as a cone
        gameManager.SpawnIgniter(gameObject.transform.position);
        gameManager.SpawnIgniter(new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y - 2, gameObject.transform.position.z - 2));
        gameManager.SpawnIgniter(new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y + 2, gameObject.transform.position.z + 2));
        SetBurnt();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Igniter") && state == BurnableObjectState.NotBurning)
        {
            SetBurning();
            other.gameObject.SetActive(false);
        }
    }
    /* Reacts to current simulation mode */
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        SimulationMode mode = simulationModeManager.mode;
        switch(mode)
        {
            case SimulationMode.Remove:
                SetNotBurning();
                gameObject.SetActive(false);
                break;
            case SimulationMode.Toggle_Fire:
                if (state == BurnableObjectState.Burning)
                {
                    SetBurnt();
                } else
                {
                    SetBurning();
                }
                break;
        }
    }
    public void ResetBurnableObject()
    {
        StopAllCoroutines();
        SetNotBurning();
        gameObject.SetActive(false);
    }
}
