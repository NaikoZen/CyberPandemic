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
    }


    void Update()
    {
       
        Move();

        AttAnimation();

        AttScore();

        Jump();
        y = rb.velocity.y;
    

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

            if (Input.GetButtonDown("Jump"))
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
            
            scorepoints++;
            AttScore();
            Debug.Log($"Player {NetworkObjectId} marcou pontos!");
        }
        
    }


    //score points after death
    public void ScoretoPlayer()
    {
        HealthSystem propriaVida = GetComponent<HealthSystem>();
        if (propriaVida.maxHealth > 0)
        {
            PegouScorepoints();
            AttScore();
           
        }

    }
    public void AttScore()
    {
        //Debug.Log("tome ponto");
        //scorepoints_text.text = scorepoints.ToString();
    }





}
