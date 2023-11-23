using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

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


        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);


    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Untagged"))
        {
            Debug.Log("destruira");
            Destroy(gameObject);

        }




    }


}