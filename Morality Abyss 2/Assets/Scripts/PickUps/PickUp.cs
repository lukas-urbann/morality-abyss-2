using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpType type;
    public int retrieveValue = 0;
    public bool interactable;
    public string retrieveString;

    public enum PickUpType
    {
        Ammo_AssaultRifle,
        Ammo_Handgun,
        Health,
        Generic,
    };
}
