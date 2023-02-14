using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapons : MonoBehaviour
{
    [SerializeField] private float force = 4;
    [SerializeField] private float damage = 1;
    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float spreadConfig = 0.1f;

   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var randomX = Random.Range(-spreadConfig / 2, spreadConfig / 2);
            var randomY = Random.Range(-spreadConfig / 2, spreadConfig / 2);
            var spread = new Vector3(randomX, randomY, 0f);
            Vector3 direction = shootPoint.forward + spread;


            if (Physics.Raycast(shootPoint.position, direction, out var hit))
            {
                print(hit.transform.gameObject.name);

                var impactEffect =
                    Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
                Destroy(impactEffect,0.5f);

                var destructible = hit.transform.GetComponent<DestructibleObject>();

                if (destructible != null)
                {
                    destructible.ReceiveDamage(damage);
                }

                var rigidbody = hit.transform.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.AddForce(shootPoint.forward * force, ForceMode.Impulse);
                }
            }
           

        }
    }
}
