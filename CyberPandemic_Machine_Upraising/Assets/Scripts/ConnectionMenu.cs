using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ConnectionMenu : MonoBehaviour
{

    //Script do Menu - Botoes e suas funcionalidades...

    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject playerHud;

    //verificar se esta em game ou no menu
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject Pause;
    [SerializeField] public GameObject Guide;
    [SerializeField] public GameObject Vitoria;
    [SerializeField] public GameObject Derrota;
    [SerializeField] public GameObject preScreen;


    //informa se esta em jogo ou n�o.
    public bool startGame;

   
   

    private void Update()
    {
        OpenPause();
    }
   



    public void ConectarCliente()
    {
      
         NetworkManager.Singleton.StartClient();
        playerHud.SetActive(true);
        startGame = true;


    }


    public void ConectarHost()
    {
      
        NetworkManager.Singleton.StartHost();
        playerHud.SetActive(true);
        startGame = true;
    }

    public void OpenCredits()
    {

        creditsPanel.SetActive(true);

    }


    public void CloseCredits()
    {

        creditsPanel.SetActive(false);

    }

    
    //PAUSE
    public void OpenPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!Menu.activeSelf && !Pause.activeSelf)
            {

                Pause.SetActive(true);



            }
            else
            {

                Pause.SetActive(false);

            }





        }
    }

    public void ClosePause()
    {
        Pause.SetActive(false);
        
    }

    public void BackMenu()
    {
        
        Pause.SetActive(false);
        Menu.SetActive(true);
    }


    public void QuitGame()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }

    public void OpenGuide()
    {
        Guide.SetActive(true);
        Pause.SetActive(false);

    }
   
    public void CloseGuide()
    {
        Guide.SetActive(false);
        Pause.SetActive(true);
    }
  

    




}
