using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igniter : MonoBehaviour
{
    [SerializeField]
    float timeToLive = 3.0f;
    private void Start()
    {
        StartCoroutine("TimeToLive");
    }
    IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
