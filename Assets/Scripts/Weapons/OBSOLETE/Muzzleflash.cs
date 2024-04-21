using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Muzzleflash : MonoBehaviour
    {
        private float _rotationDegrees;
        private Transform _rotationX;

        private void Start()
        {
            _rotationX = gameObject.GetComponent<Transform>();
        }
        
        private void OnEnable()
        {
            _rotationDegrees = Random.Range(0, 360);
            _rotationX.transform.rotation = Quaternion.Euler(_rotationDegrees, -90, 90);
        }
    }
}