using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
   
    //script utilizado para subsituir o NetworkTransform, este informa que o Server não tem um Host Inicial.
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }


}
