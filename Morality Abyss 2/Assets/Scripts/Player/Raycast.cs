using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Raycast : MonoBehaviour
    {
        private Player.UI _ui;
        private Player.Controller _pController;
        public AudioClip pickup;

        void Start()
        {
            _ui = gameObject.transform.root.GetComponent<Player.UI>();
            _pController = gameObject.transform.root.GetComponent<Player.Controller>();
        }
        
        private void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);

                if (PickCheck(hit))
                {
                    _ui.middleText.text = hit.collider.GetComponent<PickUp>().retrieveString;
                    _ui.ShowMiddleText(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _pController.properties.AudioSource.PlayOneShot(pickup);
                        switch (hit.collider.GetComponent<PickUp>().type)
                        {
                            case PickUp.PickUpType.Health:
                                _pController.properties.SET_health(hit.collider.GetComponent<PickUp>().retrieveValue);
                                hit.collider.gameObject.SetActive(false);
                                break;
                            case PickUp.PickUpType.Ammo_Handgun:
                                _pController.pistolProps.amount += hit.collider.GetComponent<PickUp>().retrieveValue;
                                hit.collider.gameObject.SetActive(false);
                                break;
                            case PickUp.PickUpType.Ammo_AssaultRifle:
                                _pController.assaultRifleProps.amount += hit.collider.GetComponent<PickUp>().retrieveValue;
                                hit.collider.gameObject.SetActive(false);
                                break;
                            case PickUp.PickUpType.Generic:
                                _pController.hasKey = true;
                                hit.collider.gameObject.SetActive(false);
                                break;
                            default:
                                Debug.Log("illegalni vec");
                                break;
                        }
                    }
                }
                else
                {
                    _ui.ShowMiddleText(false);
                }
            }

            bool PickCheck(RaycastHit hit)
            {
                var canPick = hit.collider.GetComponent<PickUp>();

                if (canPick != null && canPick.interactable)
                    return true;
                else
                    return false;
            }
        }
    }
}