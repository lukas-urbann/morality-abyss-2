using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (other.collider.GetComponent<Player.Controller>().hasKey)
            {
                anim.SetTrigger("isOpen");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player.Controller>().hasKey)
            {
                other.gameObject.GetComponent<Player.Controller>().hasKey = false;
                anim.SetTrigger("isOpen");
            }
        }
    }
}
