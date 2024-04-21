using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Colliders
{
    public class CollideTrigger : MonoBehaviour
    {
        public ActionType aType;
        public enum ActionType
        {
            PlaySong,
            SpawnEnemy,
            LevelChange,
        };

        public AudioClip selectedSong;
        private AudioSource audioSource;
        public Transform enemySpawnpoint;
        public GameObject enemy;
        public string levelString;

        void Start()
        {
            audioSource = GameObject.Find("JukeBox").GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerEvent();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TriggerEvent();
            }
        }

        public void TriggerEvent()
        {
            switch (aType)
            {
                case ActionType.PlaySong:
                    audioSource.clip = selectedSong;
                    audioSource.Play();
                    break;
                case ActionType.SpawnEnemy:
                    Instantiate(enemy, enemySpawnpoint.position, enemySpawnpoint.rotation);
                    break;
                case ActionType.LevelChange:
                    SceneManager.LoadScene(levelString);
                    break;
            }
            Destroy(gameObject);
        }
    }
}