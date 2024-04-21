using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Ammo : MonoBehaviour
    {
        public int currentAmount, amount, magCapacity;

        public void Reload()
        {
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