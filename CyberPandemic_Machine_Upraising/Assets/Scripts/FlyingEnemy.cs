using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FlyingEnemy : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float shootingInterval = 2f;
    [SerializeField] GameObject projectilePrefab;

    private float timeSinceLastShot = 0f;

    void Update()
    {
        // Somente o servidor controla o comportamento do inimigo
        if (IsServer)
        {
            MoveTowardsPlayer();
            ShootAtPlayers();
        }
    }

    void MoveTowardsPlayer()
    {
        // Encontra os jogadores na cena
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Move em direção ao primeiro jogador encontrado (pode ser expandido para selecionar o jogador mais próximo)
        if (players.Length > 0)
        {
            Vector3 playerPosition = players[0].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);

            // Rotaciona em direção ao jogador
            Vector3 direction = playerPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ShootAtPlayers()
    {
        timeSinceLastShot += Time.deltaTime;

        // Dispara em intervalos regulares
        if (timeSinceLastShot >= shootingInterval)
        {
            // Encontra os jogadores na cena
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // Atira em direção a cada jogador encontrado
            foreach (GameObject player in players)
            {
                Vector3 shootDirection = player.transform.position - transform.position;
                Quaternion shootRotation = Quaternion.LookRotation(shootDirection, Vector3.up);

                // Instancia o projétil
                GameObject projectile = Instantiate(projectilePrefab, transform.position, shootRotation);

                // Spawna o projétil na rede
                projectile.GetComponent<NetworkObject>().Spawn();
            }

            // Reinicia o temporizador de tiro
            timeSinceLastShot = 0f;
        }
    }
}