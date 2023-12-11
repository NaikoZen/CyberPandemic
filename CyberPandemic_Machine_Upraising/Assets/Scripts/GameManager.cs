using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class GameManager : NetworkBehaviour
{
    //SCORES

    

    //TIMER
    public float timer;
    public TMP_Text timerText;

    void Update()
    {
        AttTimer();
        GameplayVerify();

    }

    //verifica se o startGame e true no connectionMenu -> se for true entao esta em game.
    public void GameplayVerify()
    {
        ConnectionMenu cM = GetComponent<ConnectionMenu>();
        if (cM.startGame == true)
        {
            timer += Time.deltaTime;

        }
        else { return; }
    }

    public void AttTimer()
    {

        timerText.text = timer.ToString();
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 100) % 100);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void IsDied()
    {
        // Obter todos os NetworkObjects no servidor
        NetworkObject[] allNetworkObjects = FindObjectsOfType<NetworkObject>();
        HealthSystem healthSystem = GetComponent<HealthSystem>();

    }

}
