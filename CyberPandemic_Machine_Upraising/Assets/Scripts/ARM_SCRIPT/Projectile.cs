using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
{
    //este script serve para a Bullet
    private Vector3 firingPoint;

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
        
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }

    }



}