using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEditor;
using UnityEngine;
using Vars = Player.Vars;

namespace Weapons
{
    public class Shooting : MonoBehaviour
    {
        private Animator _anim;
        public AudioClip assaultRifleGunshot, pistolGunshot;
        public AudioSource audioSource;
        private Player.Controller _pController;
        private Menus.InGame_Menu menu;
        public float waitTime;
        private float _origWaitTime;
        public ParticleSystem muzzleflash;
        public Weapon reloadCheck;

        public WeaponType weapon;
        public enum WeaponType
        {
            AssaultRifle,
            Pistol,
        };
        
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            _anim = GetComponent<Animator>();
            _origWaitTime = waitTime;
            _pController = gameObject.transform.root.gameObject.GetComponent<Player.Controller>();
            menu = gameObject.transform.root.gameObject.GetComponent<Menus.InGame_Menu>();
        }

        void Update()
        {
            waitTime -= 1 * Time.deltaTime;
            int bullets = 0;

            switch (weapon)
            {
                case WeaponType.Pistol:
                    bullets = _pController.pistolProps.currentAmount;
                    audioSource.clip = pistolGunshot;
                    break;
                case WeaponType.AssaultRifle:
                    bullets = _pController.assaultRifleProps.currentAmount;
                    audioSource.clip = assaultRifleGunshot;
                    break;
            }
            
            if (Input.GetMouseButton(0) && waitTime < 0 && bullets > 0 && !reloadCheck.isReloading && _pController.playerStatus != Vars.PlayerState.Running && !menu._pause)
            {
                waitTime = _origWaitTime;

                switch (weapon)
                {
                    case WeaponType.Pistol:
                        _pController.pistolProps.currentAmount--;
                        _anim.Play("pistol_shoot");
                        muzzleflash.Play();
                        audioSource.Play();
                        Shoot(8,1,35);
                        break;
                    case WeaponType.AssaultRifle:
                        _pController.assaultRifleProps.currentAmount--;
                        _anim.Play("assault_rifle_shoot");
                        muzzleflash.Play();
                        audioSource.PlayOneShot(assaultRifleGunshot);
                        Shoot(15,1,35);
                        break;
                }
            }
        }
        
        public void Shoot(float damage, int bullets, int range)
        {
            RaycastHit hit;
            if (Physics.Raycast(_pController.properties.PlayerCamera.transform.position,
                _pController.properties.PlayerCamera.transform.forward, out hit, range))
            {
                waitTime = _origWaitTime;
                _pController.playerStatus = Vars.PlayerState.Shooting;
                Debug.Log(hit.transform.name);

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<Follow>().DamageIntake(-damage);
                } else if (hit.collider.gameObject.name.Equals("Meatball"))
                {
                    hit.collider.gameObject.GetComponent<Meatball>().DamageIntake(damage);
                }
            }
        }
    }
}