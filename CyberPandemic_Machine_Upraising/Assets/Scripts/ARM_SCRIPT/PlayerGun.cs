using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerGun : NetworkBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;

    public static PlayerGun Instance;

    private float lastTimeShot = 0;

    public override void OnNetworkSpawn()
    {
        Instance = GetComponent<PlayerGun>();
    }

    public void Shoot()
    {
        if (IsServer && lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            
            // Instancia o projétil no servidor.
            GameObject go = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);

            // Obtém a referência para o componente NetworkObject.
            NetworkObject networkObject = go.GetComponent<NetworkObject>();

            // Se o objeto foi instanciado no servidor, atribui a autoridade.
            if (networkObject != null && networkObject.NetworkObjectId != null)
            {
                networkObject.SpawnAsPlayerObject(OwnerClientId);
            }
        }
    }
}