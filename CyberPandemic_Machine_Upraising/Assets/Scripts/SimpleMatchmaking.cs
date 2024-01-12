using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMatchmaking : MonoBehaviour
{
    // Codigo tirado/adaptado do canal TARODEV no YouTube

    
    private Lobby connectedLobby;
    private QueryResponse lobbies;
    private UnityTransport transport;
    private string playerId;
    private const string JoinCodeKey = "j";
    [SerializeField] private int playerToStart = 2;
    float timer;

    private void Awake()
    {
        transport = FindObjectOfType<UnityTransport>();
    }

    public async void CreateOrJoinLobby()
    {
        await Authenticate();
        connectedLobby = await QuickJoinLobby() ?? await CreateLobby();
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        {
            if (timer > 5) // tenta mudar a cena a cada 5 segundos
            {
                ChangeScene();
                timer = 0f;
            }
        }

    }

    private async Task<Lobby> QuickJoinLobby()
    {
        try
        {
            var lobby = await Lobbies.Instance.QuickJoinLobbyAsync();

            var a = await RelayService.Instance.JoinAllocationAsync(lobby.Data[JoinCodeKey].Value);

            SetTransportAsClient(a);

            NetworkManager.Singleton.StartClient();
            return lobby;
        }
        catch (Exception e)
        {
            Debug.Log(e + "No lobbies");
            return null;
        }
    }

    private void SetTransportAsClient(JoinAllocation a)
    {
        transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);
    }

    private async Task<Lobby> CreateLobby()
    {
        try
        {
            const int maxPlayers = 50;

            var a = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);

            var options = new CreateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> { { JoinCodeKey, new DataObject(DataObject.VisibilityOptions.Public, joinCode) } }
            };
            var lobby = await Lobbies.Instance.CreateLobbyAsync("nome do lobby", maxPlayers, options);

            StartCoroutine(HeartbeatLobbyCoroutine(lobby.Id, 15));

            transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

            
            NetworkManager.Singleton.StartHost();
            return lobby;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed creating a lobby: {e}");
            return null;
        }
    }

   

    private static IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        var delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
    private async Task Authenticate()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        playerId = AuthenticationService.Instance.PlayerId;
    }

    private async void ChangeScene()
    {
        if (connectedLobby != null)
        {
            Lobby lobby = await Lobbies.Instance.GetLobbyAsync(connectedLobby.Id);
            Debug.Log(lobby.Players.Count);
            if (lobby.Players.Count == 1) // vai trocar de cena com 1 jogadores conectados
            {
                // cena com nome gameplay -- mudar de acordo com seu jogo
                NetworkManager.Singleton.SceneManager.LoadScene("Jogo_Beta", UnityEngine.SceneManagement.LoadSceneMode.Single);
                
            }
        }
    }



    private void OnDestroy()
    {
        try
        {
            StopAllCoroutines();
            if (connectedLobby != null)
            {
                if (connectedLobby.HostId == playerId) Lobbies.Instance.DeleteLobbyAsync(connectedLobby.Id);
                else Lobbies.Instance.RemovePlayerAsync(connectedLobby.Id, playerId);
            }
        }
        catch (Exception e)
        {
            Debug.Log($"error shutting down lobby {e}");
        }
    }
}
