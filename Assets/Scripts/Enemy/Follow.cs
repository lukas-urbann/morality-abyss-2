using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Follow : MonoBehaviour
    {
        private Vars _properties = new Vars();
        public GameObject enemy;
        private GameObject _player;
        private Animator _enemyAnim;
        private NavMeshAgent _agent;
        private CapsuleCollider _collider;
        public ParticleSystem explosion;
        
        [HideInInspector] public float walkSpeed, runSpeed;

        void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
            _player = GameObject.Find("Player");
            _agent = GetComponent<NavMeshAgent>();
            walkSpeed = _properties.GET_walkSpeed();
            runSpeed = _properties.GET_runSpeed();
            _enemyAnim = enemy.GetComponent<Animator>();
        }

        public void DamageIntake(float dmg)
        {
            _properties.SET_health(dmg);

            if (_properties.GET_health() <= 0)
            {
                StartCoroutine(Death());
            }
        }
        
        void Update()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < 35)
            {
                Chase();
            }
            else
            {
                Wander();
            }
        }

        void Chase()
        {
            if (_properties.GET_health() > 0)
            {
                _enemyAnim.SetBool("running", true);
                _agent.destination = _player.transform.position;

                if (Vector3.Distance(_player.transform.position, transform.position) <= 2.5f)
                {
                    _enemyAnim.SetBool("attacking", true);
                    _enemyAnim.SetBool("running", false);
                }
                else
                {
                    _enemyAnim.SetBool("attacking", false);   
                    _enemyAnim.SetBool("running", true);
                }
            }
        }

        void Wander()
        {
            _enemyAnim.SetBool("dead", false);
            _enemyAnim.SetBool("running", false);
            _enemyAnim.SetBool("walking", false);
            _enemyAnim.SetBool("attacking", false);
            
            
        }

        IEnumerator Death()
        {
            explosion.Play();
            _collider.enabled = false;
            _agent.enabled = false;
            _enemyAnim.SetBool("dead", true);
            _enemyAnim.SetBool("running", false);
            _enemyAnim.SetBool("walking", false);
            _enemyAnim.SetBool("attacking", false);
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}