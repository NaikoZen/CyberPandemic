using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using static PlayerSkinManager;

public class PlayerSkinManager : NetworkBehaviour
{
    // Enum para IDs de skins
    public enum SkinID
    {
        Skin1,
        Skin2,
        Skin3,
        Skin4,
        // Adicione mais skins conforme necess�rio
    }

    // A NetworkVariable para armazenar o ID da skin
    public NetworkVariable<SkinID> playerSkin =
        new NetworkVariable<SkinID>(SkinID.Skin1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public GameObject[] skinPrefabs; // Refer�ncia aos GameObjects da Skin
    private GameObject skinInstance;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Verifica se � o dono do jogador antes de instanciar a Skin
        if (IsOwner)
        {
            // L�gica para determinar qual skin deve ser atribu�da com base no PlayerId
            int playerId = OwnerClientId.GetHashCode();
            int skinIndex = DetermineSkinIndex(playerId);

            // Instancia a Skin localmente
            skinInstance = Instantiate(skinPrefabs[skinIndex], transform.position, transform.rotation);

            // Sincroniza automaticamente o valor da NetworkVariable com os clientes
            playerSkin.Value = (SkinID)skinIndex;
        }
    }

    private int DetermineSkinIndex(int playerId)
    {
        // Adicione l�gica para determinar o �ndice da skin com base no playerId
        // Por exemplo, usando um switch ou outro m�todo

        switch (playerId)
        {
            case 1:
                return 0; // �ndice correspondente � "Skin_01"
            case 2:
                return 1; // �ndice correspondente � "Skin_02"
            // Adicione mais casos conforme necess�rio
            default:
                return 0; // �ndice padr�o
        }
    }

    [ServerRpc]
    void UpdateVisualSkinServerRpc(SkinID skinId)
    {
        // Destroi a Skin antiga, se existir
        if (skinInstance != null)
        {
            // Despawn � tratado automaticamente quando o objeto � destru�do
            Destroy(skinInstance);
        }

        // Instancia a nova Skin localmente
        skinInstance = Instantiate(skinPrefabs[(int)skinId], transform.position, transform.rotation);

        // Adicione aqui l�gica adicional para associar a Skin ao jogador
    }

    // M�todo chamado localmente para mudar a skin
    [ClientRpc]
    public void ChangeSkinLocallyClientRpc(SkinID newSkin)
    {
        // Chame o comando de servidor para sincronizar a mudan�a de skin
        CmdSyncSkinChangeServerRpc(newSkin);
    }

    // Comando chamado no servidor para mudar a skin
    [ServerRpc]
    void CmdSyncSkinChangeServerRpc(SkinID newSkin)
    {
        // Atualize visualmente a skin em todos os clientes
        UpdateVisualSkinServerRpc(newSkin);

        // Sincroniza automaticamente o valor da NetworkVariable com os clientes
        playerSkin.Value = newSkin;
    }
}
