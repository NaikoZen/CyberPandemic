using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class ClientNetworkAnimator : NetworkAnimator
{

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

}
