using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ConnectionMenu : MonoBehaviour
{
   
    //Script do Menu - Botoes e suas funcionalidades...


    public void ConectarCliente()
    {
        NetworkManager.Singleton.StartClient();

    }


    public void ConectarHost()
    {
        NetworkManager.Singleton.StartHost();
    }




}
