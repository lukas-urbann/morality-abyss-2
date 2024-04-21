using System;
using System.Collections;
using UnityEngine;
using Player;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Weapons;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private GameObject _assaultRifleObject, _pistolObject;
        public Weapon assaultRifleProps, pistolProps;
        public Vars properties = new Vars();
        [HideInInspector] public bool isRunning = false, isCrouching = false, canUncrouch = true, canSwitch = true;
        [HideInInspector] public bool canRun = true;
        [HideInInspector] public Camera viewmodelCamera;

        [HideInInspector] public float crouchSpeed = 1f,
            walkSpeed,
            runSpeed,
            gravity,
            jumpSpeed,
            mouseSpeed,
            cameraClamp,
            rotationX;

        private GameObject _crouchCameraPoint, _originalCameraPoint, _crouchSymbol;
        private HeadBob _headBobbing;
        private float _energyRegenTimer = 5;
        public Vars.PlayerState playerStatus;
        private bool _hasAssaultRifle = false, _hasHandgun = false;
        public Health health;
        public bool hasKey = false;

        public SelectedWeapon VarsPlayerSelectedWeapon;

        public enum SelectedWeapon
        {
            Hand,
            AssaultRifle,
            Pistol
        };

        void Start()
        {
            health = GetComponent<Health>();

            _assaultRifleObject = GameObject.FindWithTag("AssaultRifle");
            _pistolObject = GameObject.FindWithTag("Handgun");

            pistolProps = _pistolObject.GetComponent<Weapon>();
            assaultRifleProps = _assaultRifleObject.GetComponent<Weapon>();

            pistolProps.amount = 10;
            pistolProps.magCapacity = 7;
            pistolProps.currentAmount = 0;

            assaultRifleProps.amount = 30;
            assaultRifleProps.magCapacity = 30;
            assaultRifleProps.currentAmount = 0;

            _crouchCameraPoint = GameObject.Find("CrouchPoint");
            _originalCameraPoint = GameObject.Find("OriginalPoint");
            _crouchSymbol = GameObject.Find("Crouch Symbol");
            _crouchSymbol.SetActive(false);
            viewmodelCamera = GameObject.Find("Viewmodel Camera").GetComponent<Camera>();
            VarsPlayerSelectedWeapon = SelectedWeapon.Hand;
            properties.CharacterController = GetComponent<CharacterController>();
            properties.AudioSource = GetComponent<AudioSource>();
            properties.footsteps = GetComponent<Footsteps>();
            properties.PlayerCamera = GetComponentInChildren<Camera>();
            _headBobbing = properties.PlayerCamera.GetComponent<HeadBob>();
            CursorControl(true);

            crouchSpeed = properties.GET_crouchSpeed();
            walkSpeed = properties.GET_walkSpeed();
            runSpeed = properties.GET_runSpeed();
            gravity = properties.GET_gravity();
            jumpSpeed = properties.GET_jumpSpeed();
            mouseSpeed = properties.GET_mouseSpeed();
            cameraClamp = properties.GET_cameraClamp();
            rotationX = properties.GET_rotationX();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "HandgunPickup":
                    _hasHandgun = true;
                    Destroy(other.gameObject);
                    break;
                case "AssaultPickup":
                    _hasAssaultRifle = true;
                    Destroy(other.gameObject);
                    break;
                default:
                    Debug.Log("default item");
                    break;
            }
        }

        void Update()
        {
            playerStatus = properties.VarsPlayerState;

            if (properties.GET_energy() <= 0)
            {
                canRun = false;
                isRunning = false;
            }

            if (!canRun)
                runSpeed = 4;
            else
                runSpeed = properties.GET_runSpeed();

            // Pohyb po souřadnicích
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Kontroluje pohyb
            if (canRun && !isCrouching)
                isRunning = Input.GetKey(KeyCode.LeftShift);

            float curSpeedX = properties.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = properties.canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = properties.MoveDirection.y;
            properties.MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && properties.canMove && properties.CharacterController.isGrounded &&
                !isCrouching)
            {
                _energyRegenTimer = 5;
                properties.SET_energy(-10, false);
                properties.footsteps.JumpAudio();
                properties.MoveDirection.y = jumpSpeed;
            }
            else
            {
                properties.MoveDirection.y = movementDirectionY;
            }

            // Gravitace
            if (!properties.CharacterController.isGrounded)
            {
                canRun = false;
                playerStatus = Vars.PlayerState.InAir;
                properties.MoveDirection.y -= gravity * Time.deltaTime;
            }

            // Pohyb po mapě
            properties.CharacterController.Move(properties.MoveDirection * Time.deltaTime);

            // Obecný pohyb
            if (properties.canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * mouseSpeed;
                rotationX = Mathf.Clamp(rotationX, -cameraClamp, cameraClamp);
                properties.PlayerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);

                if (playerStatus == Vars.PlayerState.Walking ||
                    playerStatus == Vars.PlayerState.Idle ||
                    playerStatus == Vars.PlayerState.Crouching ||
                    properties.GET_energy() < 100)
                {
                    if (properties.GET_energy() <= 100)
                    {
                        EnergyRegen();
                    }
                }

                if (properties.CharacterController.isGrounded && properties.CharacterController.velocity.magnitude > 1f)
                {
                    if (!isRunning)
                    {
                        playerStatus = Vars.PlayerState.Walking;
                    }
                    else
                    {
                        playerStatus = Vars.PlayerState.Running;
                        properties.SET_energy(-10, true);
                        _energyRegenTimer = 5;
                    }

                    if (!properties.AudioSource.isPlaying)
                        switch (isRunning && canRun)
                        {
                            case true:
                                properties.footsteps.FootstepAudio("run");
                                break;
                            case false:
                                properties.footsteps.FootstepAudio("walk");
                                break;
                        }
                }


                if (Input.GetKeyDown(KeyCode.C))
                {
                    isCrouching = !isCrouching;

                    switch (isCrouching)
                    {
                        case true:
                            properties.CharacterController.height = 1;
                            properties.PlayerCamera.transform.position = _crouchCameraPoint.transform.position;
                            _headBobbing.enabled = false;
                            canRun = false;
                            walkSpeed = crouchSpeed;
                            runSpeed = crouchSpeed;
                            _crouchSymbol.SetActive(true);
                            break;
                        case false:
                            if (canUncrouch)
                            {
                                properties.CharacterController.height = 2;
                                properties.PlayerCamera.transform.position = _originalCameraPoint.transform.position;
                                _headBobbing.enabled = true;
                                canRun = true;
                                walkSpeed = properties.GET_walkSpeed();
                                runSpeed = properties.GET_runSpeed();
                                _crouchSymbol.SetActive(false);
                            }

                            break;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) && _hasHandgun && canSwitch &&
                    playerStatus != Vars.PlayerState.Aiming && playerStatus != Vars.PlayerState.Running &&
                    playerStatus != Vars.PlayerState.InAir && playerStatus != Vars.PlayerState.Shooting &&
                    playerStatus != Vars.PlayerState.Reloading)
                    VarsPlayerSelectedWeapon = SelectedWeapon.Pistol;

                if (Input.GetKeyDown(KeyCode.Alpha2) && _hasAssaultRifle && canSwitch &&
                    playerStatus != Vars.PlayerState.Aiming && playerStatus != Vars.PlayerState.Running &&
                    playerStatus != Vars.PlayerState.InAir && playerStatus != Vars.PlayerState.Shooting &&
                    playerStatus != Vars.PlayerState.Reloading)
                    VarsPlayerSelectedWeapon = SelectedWeapon.AssaultRifle;

                if (Input.GetKeyDown(KeyCode.Alpha0) && canSwitch && playerStatus != Vars.PlayerState.Aiming &&
                    playerStatus != Vars.PlayerState.Running && playerStatus != Vars.PlayerState.InAir &&
                    playerStatus != Vars.PlayerState.Shooting && playerStatus != Vars.PlayerState.Reloading)
                    VarsPlayerSelectedWeapon = SelectedWeapon.Hand;
            }

            switch (VarsPlayerSelectedWeapon)
            {
                case SelectedWeapon.Hand:
                    _pistolObject.SetActive(false);
                    _assaultRifleObject.SetActive(false);
                    break;
                case SelectedWeapon.Pistol:
                    _pistolObject.SetActive(true);
                    _assaultRifleObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        pistolProps.Reload("pistol");
                    }

                    break;
                case SelectedWeapon.AssaultRifle:
                    _pistolObject.SetActive(false);
                    _assaultRifleObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        assaultRifleProps.Reload("assault");
                    }

                    break;
            }

            if (Input.GetMouseButton(1))
                playerStatus = Vars.PlayerState.Aiming;

            switch (playerStatus == Vars.PlayerState.Aiming)
            {
                case true:
                    isRunning = false;
                    canRun = false;
                    viewmodelCamera.fieldOfView = Mathf.Lerp(viewmodelCamera.fieldOfView, 50, 20 * Time.deltaTime);
                    properties.PlayerCamera.fieldOfView =
                        Mathf.Lerp(properties.PlayerCamera.fieldOfView, 50, 20 * Time.deltaTime);
                    walkSpeed = 3;
                    runSpeed = 3;
                    break;
                case false:
                    isRunning = false;
                    canRun = true;
                    viewmodelCamera.fieldOfView = Mathf.Lerp(viewmodelCamera.fieldOfView, 75, 20 * Time.deltaTime);
                    properties.PlayerCamera.fieldOfView =
                        Mathf.Lerp(properties.PlayerCamera.fieldOfView, 75, 20 * Time.deltaTime);
                    walkSpeed = properties.GET_walkSpeed();
                    runSpeed = properties.GET_runSpeed();
                    break;
            }
        }

        private void EnergyRegen()
        {
            _energyRegenTimer -= 1 * Time.deltaTime;

            if (_energyRegenTimer <= 0)
                properties.SET_energy(5, true);
        }

        public void CursorControl(bool set)
        {
            switch (set)
            {
                case true:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
            }
        }

        public void StartVictory()
        {
            StartCoroutine(Victory());
        }

        public IEnumerator Victory()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Victory");
        }
    }
}