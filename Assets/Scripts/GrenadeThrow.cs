using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GrenadeThrow : MonoBehaviour
    {
        [SerializeField] private float throwForce = 50f;
        [SerializeField] private GameObject grenadePrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GrenadeThrowForward();
            }
        }


        private void GrenadeThrowForward()
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }


}