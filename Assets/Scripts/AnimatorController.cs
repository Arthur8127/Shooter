using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // TP animator
    public Animator animator;
    private PlayerMove playerMove;
    private PlayerRotate playerRotate;

    private void Awake()
    {
        playerMove = transform.root.GetComponent<PlayerMove>();
        playerRotate = transform.root.GetComponent<PlayerRotate>();
    }

    private void Update()
    {
        if(playerMove)
        {
            Vector2 dir = playerMove.MoveInput;
            animator.SetFloat("MoveHorizontal", dir.x);
            animator.SetFloat("MoveVertical", dir.y);
            animator.SetBool("Run", playerMove.GetRun);
            animator.SetBool("IsGround", playerMove.IsGround);
            animator.SetBool("Crouch", playerMove.GetCrouch);
        }
        
    }
    
}
