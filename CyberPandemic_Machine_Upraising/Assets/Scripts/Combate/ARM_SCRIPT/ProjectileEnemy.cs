using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class ProjectileEnemy : NetworkBehaviour
{
    //este script serve para a Bullet
    private Vector3 firingPoint;

    [SerializeField] private int damageAmount = 1; // Nova propriedade de dano
    [SerializeField] private float projectileSpeed;

    [SerializeField] private float maxProjectileDistance;


    public override void OnNetworkSpawn()
    {
        firingPoint = transform.position;
    }




    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {


        transform.Translate(Vector3.back * projectileSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Vector3.up);


    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.name);
        if (col.gameObject.CompareTag("Player"))
        {  
            // Verifica se o objeto atingido tem um componente HealthSystem
            HealthSystem healthSystem = col.GetComponent<PlayerMovement>().healthSystem;

            if (healthSystem != null)
            {
                // Causa dano ao jogador
                healthSystem.TakeDamage(damageAmount);
                Debug.Log("Levou Dano Jogador");
            }

            // Destroi o projétil após atingir o jogador
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Untagged"))
        {
            // Destroi o projétil se atingir algo não identificado
            Destroy(gameObject);
        }
    }
}