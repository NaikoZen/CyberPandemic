using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField] public int maxHealth = 10;

    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public event System.Action<int, int> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(int damage)
    {   
        //Debug.Log("TakeDamage");
        if (!IsClient)
            return;

        currentHealth -= damage;

        // Garante que a saúde não seja menor que zero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Chama o evento de mudança de saúde
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Verifica se o objeto foi destruído
        if (currentHealth == 0)
        {
            
            Die();
        }
    }

    //evento para informar que um objeto Morreu.
    public event System.Action OnDied;

    public void Die()
    {
        // Verifique se o objeto tem a tag "Player"
        if (gameObject.CompareTag("Player"))
        {
            // Adicione este código apenas se a tag for "Player"
            ConnectionMenu gameManager = FindObjectOfType<ConnectionMenu>();
            if (gameManager != null)
            {
                gameManager.Derrota();
            }
        }
        // Adicione qualquer lógica adicional de morte aqui
        // Por exemplo, desativar o GameObject, reproduzir uma animação de morte, etc.
        gameObject.SetActive(false);
        OnDied?.Invoke();
        
    }

    



}