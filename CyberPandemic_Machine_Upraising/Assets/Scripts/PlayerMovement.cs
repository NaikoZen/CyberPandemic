using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : NetworkBehaviour
{
    public GameObject animation01;
    public GameObject animation02;
    // public Animator anim;
    // public float speed;
    public float jumpForce;

    public bool isJumping;
    public bool doubleJump;

    public bool IsMoving;
  

    // public int vidaMaxima;
    // public float vidaAtual;

    private Rigidbody rb;

    float x, y;
    private float maxYvel;
    public Transform personalCamera; //referencia da camera.

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
             
         if (IsMoving)
        {
            animation01.SetActive(false);
            animation02.SetActive(true);
        }
        else
        {
            animation01.SetActive(true);
            animation02.SetActive(false);
        }
    
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
                    Debug.Log("Pulouu");
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
}

