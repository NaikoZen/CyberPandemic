using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;

public class PlayerMovement : NetworkBehaviour
{



    [SerializeField] public HealthSystem healthSystem;
    public GameObject animation01;
    public GameObject animation02;
    public GameObject animation03;


    [SerializeField] GameObject projectilePrefab;
    // public Animator anim;

    // public float speed;
    public float jumpForce;

    public bool isJumping;
    public bool doubleJump;

    public bool IsMoving;


    // public int vidaMaxima;
    // public float vidaAtual;

    //pontos pro player
    public int scorepoints;
    public TMP_Text scorepoints_text;


    private Rigidbody rb;

    float x, y;
    private float maxYvel;
    public Transform personalCamera; //referencia da camera.


    public GameObject vida_Sprite_01;
    public GameObject vida_Sprite_02;
    public GameObject vida_Sprite_03;
    public GameObject vida_Sprite_04;
    public GameObject vida_Sprite_05;


    private void Awake()
    {
        // Verifica se está no cliente antes de subscrever ao evento
        if (IsClient)
        {
            // Adiciona um ouvinte para o evento OnHealthChanged
            healthSystem.OnHealthChanged += HandleHealthChanged;
        }
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        // Adicione qualquer lógica adicional quando a saúde do jogador muda
        Debug.Log($"Player Health: {currentHealth}/{maxHealth}");
    }

    void Start()
    {


        rb = GetComponent<Rigidbody>();

    }
    public override void OnNetworkSpawn()   //metodo executado quando o objeto � criado.
    {
        CinemachineVirtualCamera vcam = personalCamera.gameObject.GetComponent<CinemachineVirtualCamera>(); //acessando o componente da vcam.
        if (IsOwner) //se formos o dono deste objeto, a camera tem prioridade 1, se n�o, a camera tem prioridade 0.
        {
            vcam.Priority = 1;
        }
        else
        {
            vcam.Priority = 0;
        }

        base.OnNetworkSpawn();

        // Verifica se é o dono do jogador antes de instanciar a Skin
        if (IsOwner)
        {
            // Obter o ID do proprietário do jogador atual
            ulong currentOwnerId = OwnerClientId;

            // Faça algo com o ID do proprietário, por exemplo, imprimir no console
            Debug.Log($"Jogador com ID {currentOwnerId} entrou no jogo");
        }

    }


    void Update()
    {

        Move();

        Jump();
        y = rb.velocity.y;

        AttAnimation();

        AttPlayerLife();

        //AttScore();


        

        

    }
    void Move()
    {
        if (IsOwner)
        {

            float h = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
            // float v = Input.GetAxis("Vertical") * Time.deltaTime * 5f;


            if (Mathf.Abs(h) > 0.01f)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }
            transform.Translate(new Vector3(h, 0, 0));

        }




    }

    void Jump()
    {
        if (IsOwner)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {

                if (!isJumping)
                {

                    //audioS.clip = Sounds[0];
                    //audioS.Play();
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
                    doubleJump = true;
                    //anim.SetTrigger("jump");
                    //Debug.Log("Pulouu");
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isJumping = false;
            //if (y <= -8)
            {
                //TakeFallDamage();
            }
        }
        else if (isJumping)
        {
            if (rb.velocity.y < y)
            {

                y = rb.velocity.y;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isJumping = true;
        }



    }

    //animacoes

    public void AttAnimation()
    {
        if (IsMoving && !isJumping)
        {
            animation01.SetActive(false);
            animation02.SetActive(true);
            animation03.SetActive(false);
        }
        if (!IsMoving && !isJumping)
        {
            animation01.SetActive(true);
            animation02.SetActive(false);
            animation03.SetActive(false);
        }
        if (isJumping && IsMoving)
        {
            animation03.SetActive(true);
            animation02.SetActive(false);
            animation01.SetActive(false);
        }
        if (isJumping && !IsMoving)
        {
            animation03.SetActive(true);
            animation02.SetActive(false);
            animation01.SetActive(false);
        }
    }

    //SCORES


    public void PegouScorepoints()
    {
        if (IsLocalPlayer)
        {
            scoreManager.scoreCount++;
            //scorepoints++;
            //AttScore();
            Debug.Log($"Player {OwnerClientId + 1} marcou pontos!");
        }

    }


    //score points after death
    public void ScoretoPlayer()
    {
        HealthSystem propriaVida = GetComponent<HealthSystem>();
        if (propriaVida.maxHealth > 0)
        {
            PegouScorepoints();
            //AttScore();

        }

    }
    public void AttPlayerLife()
    {
        if (IsLocalPlayer)
        {

            HealthSystem propriaVida = GetComponent<HealthSystem>();
            if (propriaVida.CurrentHealth == 10)
            {
                vida_Sprite_05.SetActive(true);
                vida_Sprite_04.SetActive(false);
                vida_Sprite_03.SetActive(false);
                vida_Sprite_02.SetActive(false);
                vida_Sprite_01.SetActive(false);

            }
            if (propriaVida.CurrentHealth < 10 && propriaVida.CurrentHealth > 7)
            {
                vida_Sprite_05.SetActive(false);
                vida_Sprite_04.SetActive(true);
                vida_Sprite_03.SetActive(false);
                vida_Sprite_02.SetActive(false);
                vida_Sprite_01.SetActive(false);
            }
            if (propriaVida.CurrentHealth < 8 && propriaVida.CurrentHealth > 5)
            {
                vida_Sprite_05.SetActive(false);
                vida_Sprite_04.SetActive(false);
                vida_Sprite_03.SetActive(true);
                vida_Sprite_02.SetActive(false);
                vida_Sprite_01.SetActive(false);

            }
            if (propriaVida.CurrentHealth < 6 && propriaVida.CurrentHealth > 3)
            {
                vida_Sprite_05.SetActive(false);
                vida_Sprite_04.SetActive(false);
                vida_Sprite_03.SetActive(false);
                vida_Sprite_02.SetActive(true);
                vida_Sprite_01.SetActive(false);

            }
            if (propriaVida.CurrentHealth < 4 && propriaVida.CurrentHealth > 1)
            {
                vida_Sprite_05.SetActive(false);
                vida_Sprite_04.SetActive(false);
                vida_Sprite_03.SetActive(false);
                vida_Sprite_02.SetActive(false);
                vida_Sprite_01.SetActive(true);

            }
        }
        if (!IsLocalPlayer)
        {
            vida_Sprite_05.SetActive(false);
            vida_Sprite_04.SetActive(false);
            vida_Sprite_03.SetActive(false);
            vida_Sprite_02.SetActive(false);
            vida_Sprite_01.SetActive(false);
        }
    }




    private NetworkObject GetPlayerObjectByPlayerID(ulong playerID)
    {
        // Obter todos os NetworkObjects no servidor
        NetworkObject[] allNetworkObjects = FindObjectsOfType<NetworkObject>();

        // Procurar o NetworkObject pelo PlayerID
        foreach (NetworkObject networkObject in allNetworkObjects)
        {
            if (networkObject.NetworkObjectId == playerID)
            {
                return networkObject;
            }
        }

        return null; // Retorna null se não encontrar nenhum jogador com o PlayerID especificado
    }

}

