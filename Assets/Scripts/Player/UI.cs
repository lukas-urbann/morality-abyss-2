using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class UI : MonoBehaviour
    {
        private Controller _controller;
        [SerializeField] private Slider energyBar, healthBar;
        private Text _ammoText;
        public Text middleText;

        void Start()
        {
            middleText = GameObject.Find("MiddleText").GetComponent<Text>();
            _ammoText = GameObject.Find("Ammo").GetComponent<Text>();
            _controller = gameObject.GetComponent<Controller>();
        }
        
        void Update()
        {
            energyBar.value = (_controller.properties.GET_energy() / 100);
            healthBar.value = (_controller.properties.GET_health() / 100);

            if (_controller.VarsPlayerSelectedWeapon == Controller.SelectedWeapon.Hand)
            {
                _ammoText.enabled = false;
            }
            else
            {
                _ammoText.enabled = true;
            }

            switch (_controller.VarsPlayerSelectedWeapon)
            {
                case Controller.SelectedWeapon.Pistol:
                    _ammoText.text = _controller.pistolProps.currentAmount + "/" + _controller.pistolProps.amount;
                    break;
                case Controller.SelectedWeapon.AssaultRifle:
                    _ammoText.text = _controller.assaultRifleProps.currentAmount + "/" + _controller.assaultRifleProps.amount;
                    break;
            }
        }

        public void ShowMiddleText(bool isOn)
        {
            middleText.enabled = isOn;
        }
    }   
}