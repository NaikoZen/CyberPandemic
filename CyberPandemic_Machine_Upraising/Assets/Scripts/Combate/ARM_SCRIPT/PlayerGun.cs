using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

public class PlayerGun : NetworkBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;

    private float lastTimeShot = 0;

    void Update()
    {
        // Somente o cliente dono do jogador pode iniciar o spawn do objeto
        if (IsLocalPlayer && Input.GetMouseButton(0))
        {
            // Chama o m�todo no servidor para solicitar permiss�o para atirar
            if (lastTimeShot + firingSpeed <= Time.time)
            {
                GameObject go = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
                lastTimeShot = Time.time;
                RequestPermissionToShootServerRpc();

                //Debug.Log("aqui pegou");
            }

        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestPermissionToShootServerRpc()
    {
      

        GrantPermissionToShootClientRpc();
       
    }

    [ClientRpc]
    void GrantPermissionToShootClientRpc()
    {
        
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            if (!IsLocalPlayer)
            {
                lastTimeShot = Time.time;
                // Instancia o objeto no servidor
                GameObject go = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);

                


            }
        }
        
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnProjectileServerRpc(Vector3 position, Quaternion rotation)
    {
        // Verifica se � poss�vel disparar novamente
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            if (!IsLocalPlayer)
            {
                lastTimeShot = Time.time;
                // Instancia o objeto no servidor
                GameObject go = Instantiate(projectilePrefab, position, rotation);

                // Spawna o objeto na rede
                go.GetComponent<NetworkObject>().Spawn();


            }
        }
    }
}
