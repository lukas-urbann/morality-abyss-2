using UnityEngine;
using Weapons;

namespace Player
{
    public class Vars
    {
        public CharacterController CharacterController;
        public AudioSource AudioSource;
        public Camera PlayerCamera;
        public Footsteps footsteps;
        
        public Vector3 MoveDirection = Vector3.zero;
        
        [SerializeField]private float _crouchSpeed = 2.5f;
        [SerializeField]private float _walkSpeed = 4f;
        [SerializeField]private float _runSpeed = 7f;
        [SerializeField]private float _energy = 100f;
        [SerializeField]private float _health = 100f;
        [SerializeField]private float _gravity = 24f;
        [SerializeField]private float _jumpSpeed = 6f;
        [SerializeField]private float _mouseSpeed = 2f;
        [SerializeField]private float _cameraClamp = 80.0f;
        [SerializeField]private float _rotationX = 0;
        
        public PlayerState VarsPlayerState;
        public enum PlayerState { Idle, Crouching, Walking, Running, Shooting, Reloading, Aiming, InAir };

        public bool canMove = true;

        //GENERIC VARS
        public float GET_crouchSpeed()
        {
            return _crouchSpeed;
        }
        
        public float GET_walkSpeed()
        {
            return _walkSpeed;
        }
        
        public float GET_runSpeed()
        {
            return _runSpeed;
        }
        
        public float GET_gravity()
        {
            return _gravity;
        }
        
        public float GET_jumpSpeed()
        {
            return _jumpSpeed;
        }
        
        public float GET_mouseSpeed()
        {
            return _mouseSpeed;
        }

        public float GET_cameraClamp()
        {
            return _cameraClamp;
        }
        
        public float GET_rotationX()
        {
            return _rotationX;
        }
        //!GENERIC VARS
        
        //ENERGY
        public float GET_energy()
        {
            return _energy;
        }

        public void SET_energy(float value, bool time)
        {
            if (time)
                _energy += value * Time.deltaTime;
            else
                _energy += value;
        }
        //!ENERGY

        //HEALTH
        public float GET_health()
        {
            return _health;
        }

        public void SET_health(float value)
        {
            _health += value;
        }
        //!HEALTH
    }
}