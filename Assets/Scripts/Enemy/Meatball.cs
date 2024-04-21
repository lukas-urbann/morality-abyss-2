using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meatball : MonoBehaviour
{
    public float health = 500;
    private Player.Controller _pController;
    public ParticleSystem particleSystem;
    private int bullt = 0;
    public GameObject spawnpoint, enemy;

    void Start()
    {
        _pController = GameObject.Find("Player").GetComponent<Player.Controller>();
    }

    public void DamageIntake(float damage)
    {
        bullt++;
        health -= damage;

        if (bullt == 5)
        {
            SpawnEnemy();
            bullt = 0;
        }

        if (health <= 0)
        {
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory()
    {
        particleSystem.Play();
        gameObject.SetActive(false);
        _pController.StartVictory();
        yield return new WaitForSeconds(2);
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, spawnpoint.transform.position, spawnpoint.transform.rotation);
    }
}