using UnityEngine;
using Unity.Netcode;

public class PlayerGun : NetworkBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;

    private float lastTimeShot = 0;

    void Update()
    {
        // Somente o cliente dono do jogador pode iniciar o spawn do objeto
        if (IsLocalPlayer && Input.GetMouseButtonDown(0))
        {
            // Chama o m�todo no servidor para solicitar permiss�o para atirar
            RequestPermissionToShootServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestPermissionToShootServerRpc()
    {
        // Verifica se � poss�vel disparar novamente
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            // Concede permiss�o para atirar no cliente
            GrantPermissionToShootClientRpc();
        }
    }

    [ClientRpc]
    void GrantPermissionToShootClientRpc()
    {
        // Somente o cliente dono do jogador pode atirar
        if (IsLocalPlayer)
        {
            // Agora o cliente tem permiss�o para atirar, chama o RPC para spawnar o objeto
            SpawnProjectileServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnProjectileServerRpc()
    {
        // Verifica se � poss�vel disparar novamente
        if (lastTimeShot + firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;

            // Sincroniza a posi��o e a rota��o do firingPoint do servidor
            Vector3 firingPointPosition = firingPoint.position;
            Quaternion firingPointRotation = firingPoint.rotation;

            // Instancia o objeto no servidor
            GameObject go = Instantiate(projectilePrefab, firingPointPosition, firingPointRotation);

            // Spawna o objeto na rede
            go.GetComponent<NetworkObject>().Spawn();
        }
    }
}