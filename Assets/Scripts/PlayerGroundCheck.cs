using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private Transform playerTransfrm;
    public LayerMask groundMask;
    public bool isGround;
    private void Awake()
    {
        playerTransfrm = transform;
    }

    private void Update()
    {
        isGround = Physics.Raycast(playerTransfrm.position, Vector3.down, 0.2f, groundMask);

    }
}
