using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CrouchColliderCheck : MonoBehaviour
    {
        private Player.Controller _pController;

        private void Start()
        {
            _pController = gameObject.transform.root.GetComponent<Player.Controller>();
        }
    
        private void OnTriggerStay(Collider other)
        {
            if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Trigger") && !other.gameObject.CompareTag("AssaultRifle") && !other.gameObject.CompareTag("Handgun"))
                _pController.canUncrouch = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Trigger") && !other.gameObject.CompareTag("AssaultRifle") && !other.gameObject.CompareTag("Handgun"))
                _pController.canUncrouch = true;
        }

        private void OnCollisionStay(Collision other)
        {
            if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Trigger") && !other.gameObject.CompareTag("AssaultRifle") && !other.gameObject.CompareTag("Handgun"))
                _pController.canUncrouch = false;
        }

        private void OnCollisionExit(Collision other)
        {
            if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Trigger") && !other.gameObject.CompareTag("AssaultRifle") && !other.gameObject.CompareTag("Handgun"))
                _pController.canUncrouch = true;
        }
    }
}