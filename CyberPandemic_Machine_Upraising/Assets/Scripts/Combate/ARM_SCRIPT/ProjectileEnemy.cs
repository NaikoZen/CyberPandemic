using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class ProjectileEnemy : NetworkBehaviour
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


        transform.Translate(Vector3.back * projectileSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Vector3.up);


    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Untagged"))
        {
           // Debug.Log("destruira");
            Destroy(gameObject);

        }




    }


}