using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Weapons
{
    public class HandAnimations : MonoBehaviour
    {
        public WeaponType weapon;
        public Animator anim;
        public enum WeaponType { Pistol, AssaultRifle };
        public Player.Controller pController;

        void Start()
        {
            anim = GetComponent<Animator>();
            pController = gameObject.transform.root.GetComponent<Player.Controller>();
        }

        void Update()
        {
            switch (weapon)
            {
                case WeaponType.Pistol:
                    switch (pController.playerStatus)
                    {
                        case Vars.PlayerState.Idle:
                            anim.SetBool("inAir", false);
                            anim.SetBool("aiming", false);
                            anim.SetBool("walking", false);
                            anim.SetBool("running", false);
                            break;
                        case Vars.PlayerState.Walking:
                            anim.SetBool("inAir", false);
                            anim.SetBool("aiming", false);
                            anim.SetBool("walking", true);
                            anim.SetBool("running", false);
                            break;
                        case Vars.PlayerState.Running:
                            anim.SetBool("inAir", false);
                            anim.SetBool("aiming", false);
                            anim.SetBool("walking", false);
                            anim.SetBool("running", true);
                            break;
                        case Vars.PlayerState.InAir:
                            anim.SetBool("inAir", true);
                            anim.SetBool("aiming", false);
                            anim.SetBool("walking", false);
                            anim.SetBool("running", false);
                            break;
                        case Vars.PlayerState.Aiming:
                            anim.SetBool("inAir", false);
                            anim.SetBool("aiming", true);
                            anim.SetBool("walking", false);
                            anim.SetBool("running", false);
                            break;
                    }
                    break;
            }
        }
    }
}