using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherController : MonoBehaviour
{
    [SerializeField]
    float velocity = 20f;
    ExtinguisherManager extinguisherManager;
    Animator extinguisherAnimator;
    private void Start()
    {
        extinguisherManager = GameObject.Find("Game Manager").GetComponent<ExtinguisherManager>();
        extinguisherAnimator = GetComponent<Animator>();
        extinguisherAnimator.SetFloat("RunningMultiplier", velocity / 20);
    }
    private void Update()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Despawn Area"))
        {
            gameObject.SetActive(false);
        } else if (other.CompareTag("BurnableObject"))
        {
            BurnableObject burnableObject = other.gameObject.GetComponent<BurnableObject>();
            burnableObject.Extinguish();
        }
    }
}
