using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : NetworkBehaviour
{
    public Transform personalCamera; //referencia da camera.

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
        if (IsOwner) //utilizando o NetworkBehaviour cada um move o seu GameObject/player.
        {
            float h = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
           // float v = Input.GetAxis("Vertical") * Time.deltaTime * 5f;
            transform.Translate(new Vector3(h, 0, 0));
        }
        


    }
}
