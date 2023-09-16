using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    /**
     * Rotates an object following the target rotation which is in normal degrees
     */
    public static void RotateObject(GameObject go, Vector3 targetRotation)
    {
        go.transform.eulerAngles = Vector3.Lerp(go.transform.rotation.eulerAngles, targetRotation, 1);
    }
}