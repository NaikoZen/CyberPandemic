using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FlyingEnemy : NetworkBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] public HealthSystem healthSystem;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float shootingInterval = 2f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firingPoint; // Novo: Ponto de disparo relativo ao inimigo


   

    private float timeSinceLastShot = 0f;

    void Update()
    {
        // Somente o servidor controla o comportamento do inimigo
        if (IsServer)
        {
            // Obtém a posição do inimigo
            Vector3 shooterPosition = transform.position;

            // Move em direção ao jogador e dispara
            MoveTowardsPlayerServerRpc();
            ShootAtPlayersServerRpc(shooterPosition);
        }
    }

    [ServerRpc]
    public void MoveTowardsPlayerServerRpc()
    {
        MoveTowardsPlayerClientRpc();
    }

    [ClientRpc]
     void MoveTowardsPlayerClientRpc()
{
    // Encontra os jogadores na cena
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

    // Verifica se há jogadores na cena
    if (players.Length > 0)
    {
        // Encontra o jogador mais próximo
        GameObject closestPlayer = FindClosestPlayer(players);

        if (closestPlayer != null)
        {
            // Move em direção ao jogador mais próximo
            Vector3 playerPosition = closestPlayer.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);

            // Rotaciona em direção ao jogador
            Vector3 direction = playerPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

// Função para encontrar o jogador mais próximo
GameObject FindClosestPlayer(GameObject[] players)
{
    GameObject closestPlayer = null;
    float closestDistance = Mathf.Infinity;
    Vector3 currentPosition = transform.position;

    foreach (GameObject player in players)
    {
        float distance = Vector3.Distance(player.transform.position, currentPosition);
        if (distance < closestDistance)
        {
            closestPlayer = player;
            closestDistance = distance;
        }
    }

    return closestPlayer;
}

    [ServerRpc]
    public void ShootAtPlayersServerRpc(Vector3 shooterPosition)
    {
        // Chama o método no cliente passando a posição do inimigo
        ShootAtPlayersClientRpc(shooterPosition);
    }

    [ClientRpc]
    void ShootAtPlayersClientRpc(Vector3 shooterPosition)
    {
        timeSinceLastShot += Time.deltaTime;

        // Dispara em intervalos regulares
        if (timeSinceLastShot >= shootingInterval)
        {
            // Calcula a direção do tiro com base na posição do inimigo
            Vector3 shootDirection = shooterPosition - firingPoint.position;
            Quaternion shootRotation = Quaternion.LookRotation(shootDirection, Vector3.up);

            // Instancia o projétil usando o ponto de disparo
            GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, shootRotation);

             // Obtém o componente do sistema de saúde do jogador se atingir um jogador
             HealthSystem playerHealth = projectile.GetComponent<HealthSystem>();
            
             if (playerHealth != null)
            {   
                Debug.Log("Deu Dano");
                playerHealth.TakeDamage(damageAmount);
            }
            // Spawna o projétil na rede
            // projectile.GetComponent<NetworkObject>().Spawn();

            // Reinicia o temporizador de tiro
            timeSinceLastShot = 0f;
        }
    }

    //sobre o Score -----------
    private void Start()
    {
        //inscreve-se no evento OnDied do HealthSystem
        healthSystem.OnDied += HandleEnemyDied;
    }

    void HandleEnemyDied()
    {

        //chama a funcao PegouScorePoints do PlayerMovement.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null )
        {
            playerMovement.PegouScorepoints();
        }

    }




}