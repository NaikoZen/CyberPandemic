using Unity.Netcode;
using UnityEngine;

public class aimtentativa : NetworkBehaviour
{

    private void Update()
    {
        AimRotation();
      //  ShootInput();
    }


    //rota��o do Bra�o
    public void AimRotation()
    {
        if (IsOwner)
        {
            //RaycastHit � a linha abaixo do mouse, uma mira.
            RaycastHit _hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //quando o raycast colidir em algo, o bra�o vai "olhar" para onde o Mouse est� no momento, necess�rio um Background para colidir com o Raycast.
            if (Physics.Raycast(ray, out _hit))
            {
                //faz o objeto "OLHAR" para o MOUSE.
                transform.LookAt(new Vector3(_hit.point.x, _hit.point.y, transform.position.z));
            }
        }

    }


    //O Update vai chamar esta fun��o quando o bot�o esquerdo do mouse for apertado "Fire1" 
   // public void ShootInput()
  //  {

        // Somente o servidor ou o host podem iniciar o spawn do objeto
    //    if ((IsLocalPlayer) && Input.GetMouseButtonDown(0))
   //     {
            // Chama o m�todo no servidor para spawnar o objeto
   //         PlayerGun.Instance.SpawnProjectileServer();
  //      }
   // }




}
