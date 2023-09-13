using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BurnableObject : MonoBehaviour
{
    BurnableObjectState state = BurnableObjectState.NotBurning;
    Renderer objectRenderer;
    public float burningDuration = 5.0f;
    GameManager gameManager;
    
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SetNotBurning();
    }

    /**
     * Sets the status to not burning and changes it to the green color
     */
    public void SetNotBurning()
    {
        if (state == BurnableObjectState.Burnt) return;
        state = BurnableObjectState.NotBurning;
        objectRenderer.material.color = new Color(0.13f, 0.54f, 0.13f);
    }
    /**
     * Sets the status to burning, changes the color to red, and triggers countdown to next status which is Burnt
     */
    public void SetBurning()
    {
        if (state != BurnableObjectState.NotBurning) return;
        state = BurnableObjectState.Burning;
        objectRenderer.material.color = new Color(0.50f, 0.0f, 0.0f);
        // trigger countdown for next state of burnt
        StartCoroutine("Burning");
    }
    /**
     * Sets status to burnt and changes color to black
     */
    public void SetBurnt()
    {
        if (state == BurnableObjectState.Burnt) return;
        state = BurnableObjectState.Burnt;
        objectRenderer.material.color = new Color(0.0f, 0.0f, 0.0f);
    }
    /*
     * After the object starts burning, triggers countdown to be burn
     */
    IEnumerator Burning()
    {
        yield return new WaitForSeconds(burningDuration);
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
        }
    }
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        SimulationMode mode = gameManager.GetComponent<SimulationModeManager>().mode;
        switch(mode)
        {
            case SimulationMode.Remove:
                gameObject.SetActive(false);
                break;
            case SimulationMode.ToggleFire:
                SetBurning();
                break;
        }
    }

}
