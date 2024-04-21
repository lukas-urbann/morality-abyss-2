using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    void Awake()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
