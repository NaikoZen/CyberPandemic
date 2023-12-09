using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.Networking.Transport;

public class ConnectionMenu : NetworkBehaviour
{

    //Script do Menu - Botoes e suas funcionalidades...

    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject playerHud;

    //verificar se esta em game ou no menu
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject Pause;
    [SerializeField] public GameObject Guide;
    [SerializeField] public GameObject Vitoria;
    [SerializeField] public GameObject derrota;
    [SerializeField] public GameObject preScreen;
    [SerializeField] public GameObject scores_Finals;


    //SCORE -> volta a zero.
    private scoreManager scoreManager;


    //informa se esta em jogo ou n�o.
    public bool startGame;

    private void Start()
    {
        scoreManager = FindObjectOfType<scoreManager>();
    }



    private void Update()
    {
        OpenPause();
    }




    public void ConectarCliente()
    {

        NetworkManager.Singleton.StartClient();
        playerHud.SetActive(true);
        startGame = true;
        scoreManager.scoreCount = 0;

        Debug.Log($"Player {NetworkObjectId} Entrou");


    }


    public void ConectarHost()
    {

        NetworkManager.Singleton.StartHost();
        playerHud.SetActive(true);
        startGame = true;
        scoreManager.scoreCount = 0;
        Debug.Log($"Player {NetworkObjectId} é o Dono da Partida ");
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
        RestartScene();
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

    public void Derrota()
    {
        // Ative a tela de game over quando o jogador morrer

        derrota.SetActive(true);
        Invoke("DesativarTelaDerrota", 4f);
        Invoke("Ativar_Scores_Finals", 4f);
    }

    public void WaveCompleta()
    {
        // Ative a tela de game over quando o jogador morrer

        Vitoria.SetActive(true);
        // Desativa a tela de vitória após 4 segundos
        Invoke("DesativarTelaVitoria", 4f);
    }

    private void DesativarTelaVitoria()
    {

        Vitoria.SetActive(false);

    }

    public void DesativarTelaDerrota()
    {
        derrota.SetActive(false);
    }
    public void Ativar_Scores_Finals()
    {
        
        scores_Finals.SetActive(true);
    }

    public void RestartScene()
    {
        // Reinicie a cena quando o botão for clicado
        NetworkManager.Singleton.DisconnectClient(OwnerClientId);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





}
