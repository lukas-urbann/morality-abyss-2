using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Health : MonoBehaviour
    {
        private Player.Controller _pController;
        private Player.Death _death;

        void Start()
        {
            _pController = GetComponent<Player.Controller>();
            _death = GetComponent<Death>();
        }

        public void DamageIntake(int damage)
        {
            _pController.properties.SET_health(damage);

            if (_pController.properties.GET_health() <= 0)
            {
                _death.DeathEvent();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyHit"))
            {
                DamageIntake(-15);
            }
        }
    }
}