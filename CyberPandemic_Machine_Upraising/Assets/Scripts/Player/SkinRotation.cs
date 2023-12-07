using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SkinRotation : NetworkBehaviour
{

    [SerializeField] private Transform deOlho;



    //para onde o Player esta olhando
    private Quaternion facingRight;
    private Quaternion facingLeft;








    void Start()
    {

        //facingLeft � a Escala do Objeto - se a Scale de x = -1 esta olhando para Esquerda;
        facingRight = transform.localRotation;

        facingLeft = transform.localRotation;
        //facingLeft.x = facingLeft.y = 180;
        facingLeft.y = facingLeft.y = 180;

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
                 transform.localRotation = facingRight;
                //Debug.Log("olhou direita");


            }
            if (r < 0)
            {
                //olhando para a Esquerda
                 transform.localRotation = facingLeft;
                //Debug.Log("olhou esquerda");
            }



        }

    }





}
