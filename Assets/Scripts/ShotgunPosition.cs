using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPosition : MonoBehaviour
{
    [SerializeField]
    Transform shotgunPos;
    [SerializeField]
    Transform playerPos;

    void FixedUpdate()
    {
        if(shotgunPos.position != shotgunPos.position + new Vector3(5f, 1f, -1.5f))
        {
            shotgunPos.position = shotgunPos.position + new Vector3(5f, 1f, -1.5f);
        }
    }
}
