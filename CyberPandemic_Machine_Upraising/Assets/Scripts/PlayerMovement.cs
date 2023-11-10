using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    

 
    void Update()
    {
        if (IsOwner) //utilizando o NetworkBehaviour cada um move o seu GameObject/player.
        {
            float h = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
            float v = Input.GetAxis("Vertical") * Time.deltaTime * 5f;
            transform.Translate(new Vector3(h, 0, v));
        }
        


    }
}
