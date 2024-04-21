using System.Collections;
using System.Collections.Generic;
using System.Net;
using Player;
using UnityEngine;

namespace Weapons
{
    public class AimDownsights : MonoBehaviour
    {
        private Player.Controller _pController;
        public Vector3 hipFire, downSights;

        void Start()
        {
            _pController = gameObject.transform.root.GetComponent<Player.Controller>();
        }
        
        void Update()
        {
            if (_pController.playerStatus == Vars.PlayerState.Aiming)
            {
                transform.position = downSights;
                print("downsights");
            }
            else
            {
                transform.position = hipFire;
                print("hipfire");
            }
        }
    }
}