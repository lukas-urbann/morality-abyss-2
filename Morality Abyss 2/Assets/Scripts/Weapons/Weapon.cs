using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        private Player.Controller _pController;
        public float reloadTime;
        public int currentAmount, amount, magCapacity;
        public Animator anim;
        public bool isReloading = false;

        void Start()
        {
            _pController = gameObject.transform.root.GetComponent<Player.Controller>();
        }
        
        public void Reload(string gunType)
        {
            StartCoroutine(ReloadProcess(gunType));
        }

        IEnumerator ReloadProcess(string gunType)
        {
            _pController.canSwitch = false;
            isReloading = true;
            switch (gunType)
            {
                case "assault":
                    anim.Play("assault_rifle_reload");
                    break;
                case "pistol":
                    anim.Play("pistol_reload");
                    break;
            }
            
            yield return new WaitForSeconds(reloadTime);
            _pController.canSwitch = true;
            isReloading = false;
            
            amount += currentAmount;
            currentAmount = 0;

            if (amount > magCapacity)
            {
                currentAmount = magCapacity;
                amount -= magCapacity;
            }
            else
            {
                currentAmount = amount;
                amount = 0;
            }
        }
    }
}