using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position+ Camera.main.transform.forward);
    }
}
