using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Vars
    {
        [SerializeField]private float _walkSpeed = 5f;
        [SerializeField]private float _runSpeed = 8.5f;
        [SerializeField]private float _health = 50f;

        public float GET_walkSpeed()
        {
            return _walkSpeed;
        }
        
        public float GET_runSpeed()
        {
            return _runSpeed;
        }
        
        public float GET_health()
        {
            return _health;
        }

        public void SET_health(float value)
        {
            _health += value;
        }
    }
}