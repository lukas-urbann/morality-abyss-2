using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


namespace VarSaver
{
    public class GlobalVar : MonoBehaviour
    {
        public static GlobalVar Instance;
        public Player.Controller _pController;
        
        public float hp;
        public float energy;
        public Ammo assaultRifle, pistol;

        void Awake ()   
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }
        }
        
        void Start () 
        {   
            hp = GlobalVar.Instance.hp;
            energy = GlobalVar.Instance.energy;
        }
        
        public void SavePlayer()
        {
            GlobalVar.Instance.hp = hp;
            GlobalVar.Instance.energy = energy;
        }
    }
}