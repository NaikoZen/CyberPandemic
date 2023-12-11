using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Networking.Transport;

public class SpawnPlayerOnSceneLoad : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if(IsHost)
        {
            NetworkManager.Singleton.StartHost();
        }
        if(IsClient)
        {
            NetworkManager.Singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
