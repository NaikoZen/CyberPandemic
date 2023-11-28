using Unity.Netcode;
using UnityEngine;

public class aimtentativa : NetworkBehaviour
{

    private void Update()
    {
        AimRotation();
      
    }


    //rotação do Braço
    public void AimRotation()
    {
        if (IsOwner)
        {
            //RaycastHit é a linha abaixo do mouse, uma mira.
            RaycastHit _hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //quando o raycast colidir em algo, o braço vai "olhar" para onde o Mouse está no momento, necessário um Background para colidir com o Raycast.
            if (Physics.Raycast(ray, out _hit))
            {
                //faz o objeto "OLHAR" para o MOUSE.
                transform.LookAt(new Vector3(_hit.point.x, _hit.point.y, transform.position.z));
            }
        }

    }


   




}
