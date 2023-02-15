using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private float delay = 3f;
        [SerializeField] private float radius = 5f;
        [SerializeField] private float force = 700f;
        [SerializeField] private float damage = 80f;


        public GameObject explosionEffect;

        private float countdown;
        private bool hasExploded = false;

        void Start()
        {
            countdown = delay;
        }

        private void Update()
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }

        private void Explode()
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                var destructible = nearbyObject.GetComponent<DestructibleObject>();
                if (destructible != null)
                {
                    destructible.ReceiveDamage(damage);
                }
            }
            
            
            Destroy(gameObject);
        }
        
        
        
    }
}