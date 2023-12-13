using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Netcode;
using UnityEngine;

public class WaveSpawner : NetworkBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        //nome da Wave
        public string name;

        //inimmigo que sera spawnado.
        public NetworkObject enemyPrefab;

        //quantidade de inimmigos spawnados.
        public int count;

        //segundos esperados para proxima wave, depois de todos os inimigos mortos.
        public float rate;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn points referenced");
        }

        ConnectionMenu cM = GetComponent<ConnectionMenu>();
        if (cM.startGame == true)
        {

            waveCountdown = timeBetweenWaves;

        }
        else { return; }
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {

                WaveCompleted();
            }
            return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                ConnectionMenu cM = GetComponent<ConnectionMenu>();
                if (cM.startGame == true)
                {

                    StartCoroutine(SpawnWave(waves[nextWave]));


                }
                else { return; }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completada!");
        ConnectionMenu gameManager = FindObjectOfType<ConnectionMenu>();
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (gameManager != null)
        {
            gameManager.WaveCompletaServerRPC();
        }

        if (nextWave + 1 > waves.Length - 1)
        {

            nextWave = 0;
            Debug.Log("all waves complete looping...");
        }
        else
        {
            nextWave++;
        }


    }


    //verifica se os inimigos est�o vivos, para comecar outra wave.
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }



    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);

        }

        state = SpawnState.WAITING;

        yield break;
    }




    void SpawnEnemy(NetworkObject enemyPrefab)
    {
        Debug.Log("Spawning Enemy: " + enemyPrefab.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];


        Vector3 spawnPosition = _sp.position;
        Quaternion spawnRotation = _sp.rotation;


        // Spawn do inimigo usando NetworkObject
        NetworkObject networkObject = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        // Verifica se a inst�ncia n�o � nula e a spawna na rede
        if (networkObject != null)
            networkObject.Spawn();
    }

}