using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class ConnectionMenu : MonoBehaviour
{

    //Script do Menu - Botoes e suas funcionalidades...

    [SerializeField] private GameObject creditsPanel;




    public void ConectarCliente()
    {
      
        NetworkManager.Singleton.StartClient();

    }


    public void ConectarHost()
    {
      
        NetworkManager.Singleton.StartHost();
    }

    public void OpenCredits()
    {

        creditsPanel.SetActive(true);

    }


    public void CloseCredits()
    {

        creditsPanel.SetActive(false);

    }


    public void QuitGame()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }




}
