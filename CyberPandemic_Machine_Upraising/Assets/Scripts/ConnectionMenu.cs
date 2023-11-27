using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class ConnectionMenu : MonoBehaviour
{

    //Script do Menu - Botoes e suas funcionalidades...

    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject playerHud;




    public void ConectarCliente()
    {
      
        NetworkManager.Singleton.StartClient();
        playerHud.SetActive(true);

    }


    public void ConectarHost()
    {
      
        NetworkManager.Singleton.StartHost();
        playerHud.SetActive(true);
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
