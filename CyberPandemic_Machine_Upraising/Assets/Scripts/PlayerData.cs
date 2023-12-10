using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerData : NetworkBehaviour
{
    NetworkVariable<int> Score = new NetworkVariable<int>();
    NetworkVariable<FixedString128Bytes> Name = new NetworkVariable<FixedString128Bytes>();
}
