using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class Projectile : NetworkBehaviour
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


        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);


    }

    void OnTriggerEnter(Collider col)
   {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colidiu com o Inimigo");
            // Verifica se o objeto atingido tem um componente HealthSystem
            HealthSystem healthSystem = col.GetComponent<FlyingEnemy>().healthSystem;
            
            if (healthSystem != null)
            {
                // Causa dano ao jogador
                healthSystem.TakeDamage(damageAmount);
                Debug.Log("Levou Dano Inimigo");
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