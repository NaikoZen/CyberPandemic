using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerGun : NetworkBehaviour
{
    //este script serve para uma mão com uma arma, ou a propria arma.

    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;

    

    //esta CLASSE é publica pois é chamada por outro script
    public static PlayerGun Instance;

    private float lastTimeShot = 0;

    //evitar ao máximo usar "em jogos de rede" o AWAKE.
    public override void OnNetworkSpawn()
    {
        Instance = GetComponent<PlayerGun>();
        
    }

    

    public void Shoot()
    {
       
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
           GameObject go =  Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            go.GetComponent<NetworkObject>().SpawnAsPlayerObject(OwnerClientId);
        }
       
    }
}
