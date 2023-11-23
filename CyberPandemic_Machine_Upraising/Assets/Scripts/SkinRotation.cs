using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SkinRotation : NetworkBehaviour
{

    [SerializeField] private Transform deOlho;



    //para onde o Player esta olhando
    private Vector3 facingRight;
    private Vector3 facingLeft;








    void Start()
    {

        //facingLeft é a Escala do Objeto - se a Scale de x = -1 esta olhando para Esquerda;
        facingRight = transform.localScale;

        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;


    }

    // Update is called once per frame
    void Update()
    {
        RotateSkin();

    }

    public void RotateSkin()
    {

        if (IsOwner)
        {
            float r = deOlho.rotation.y;

            //olhar para Direita e Esquerda.
            if (r > 0)
            {
                //olhando para a Direita
                 transform.localScale = facingRight;
                Debug.Log("olhou direita");


            }
            if (r < 0)
            {
                //olhando para a Esquerda
                 transform.localScale = facingLeft;
                Debug.Log("olhou esquerda");
            }



        }

    }





}
