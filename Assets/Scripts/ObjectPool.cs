using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> burnableObjectsPool;
    public List<GameObject> igniterPool;
    public GameObject burnableObjectToPool;
    public GameObject igniterToPool;
    public int amountBurnableObjectToPool;
    public int amountIgniterToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        burnableObjectsPool = new List<GameObject>();
        igniterPool = new List<GameObject>();
        for (int i = 0; i < amountBurnableObjectToPool; i++)
        {
            PoolBurnableObject();
        }
        for (int i = 0; i < amountIgniterToPool; i++)
        {
            PoolIgniter();
        }
    }
    public GameObject GetBurnableObjectPooled()
    {
        for (int i = 0; i < amountBurnableObjectToPool; i++)
        {
            if (!burnableObjectsPool[i].activeInHierarchy)
            {
                return burnableObjectsPool[i];
            }
        }
        // If nothing is found on the pool, more objects need to be pooled
        return PoolBurnableObject();
    }
    private GameObject PoolBurnableObject()
    {

        GameObject tmp = Instantiate(burnableObjectToPool);
        tmp.SetActive(false);
        burnableObjectsPool.Add(tmp);
        return tmp;
    }

    public GameObject GetIgniterPooled()
    {
        for (int i = 0; i < amountIgniterToPool; i++)
        {
            if (!igniterPool[i].activeInHierarchy)
            {
                return igniterPool[i];
            }
        }
        // If nothing is found on the pool, more objects need to be pooled
        return PoolIgniter();
    }
    private GameObject PoolIgniter()
    {

        GameObject tmp = Instantiate(igniterToPool);
        tmp.SetActive(false);
        igniterPool.Add(tmp);
        return tmp;
    }
}
