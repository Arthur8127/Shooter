using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed, runSpeed, flySpeed;
    public float sideSpeed, backSpeed, crouchSpeed;
    public float energySpeed;
    [SerializeField]
    [Range(0f, 20f)]
    private float currentSpeed;

    private CharacterController characterController;
    private Vector3 moveDirection, velocity;    
    private Transform playerTransform;
    private PlayerGroundCheck groudCheck;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool isRun;
    private bool isCrouch;
    public float upHeight, crouchHeight;
    public Vector3 camUpPos, camCrouchPos;
    public Transform head;
   

    public Vector2 MoveInput
    {
        get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); }
    }
    public Vector2 MoveInputRaw
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); }
    }

    public bool GetRun
    { get { return isRun; } }

    public bool IsGround
    { get { return groudCheck.isGround; } }

    public bool GetCrouch
    { get { return isCrouch; } }

    private void Awake()
    {
        groudCheck = GetComponent<PlayerGroundCheck>();
        playerTransform = transform;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {


        if (groudCheck.isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        moveDirection = (playerTransform.forward * MoveInputRaw.y + playerTransform.right * MoveInputRaw.x).normalized;
        isRun = Input.GetKey(KeyCode.LeftShift) && MoveInputRaw == Vector2.up && groudCheck.isGround && !isCrouch;
        isCrouch = Input.GetKey(KeyCode.LeftControl) && IsGround && !isRun;

        TryCrouch();
        GetSpeed();

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && groudCheck.isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }

    private void GetSpeed()
    {
        if (isRun) currentSpeed = runSpeed;
        else if (isCrouch) currentSpeed = crouchSpeed;
        
        else
        {
            if (MoveInputRaw == Vector2.up || MoveInputRaw == Vector2.up + Vector2.left || MoveInputRaw == Vector2.up + Vector2.right)
                currentSpeed = moveSpeed;
            else if (MoveInputRaw == Vector2.right || MoveInputRaw == Vector2.left)
                currentSpeed = sideSpeed;
            else if (MoveInputRaw == Vector2.down || MoveInputRaw == Vector2.down + Vector2.left || MoveInputRaw == Vector2.down + Vector2.right)
                currentSpeed = backSpeed;
        }
    }

    private void TryCrouch()
    {
        if(isCrouch && characterController.height != crouchHeight)
        {
            
            StopAllCoroutines();
            characterController.height = crouchHeight;
            characterController.center = Vector3.up * (crouchHeight / 2f);
            StartCoroutine(HeadChangePosition(camCrouchPos));
        }
        else if(!isCrouch && characterController.height != upHeight)
        {
            StopAllCoroutines();
            characterController.height = upHeight;
            characterController.center = Vector3.up * (upHeight / 2f);
            StartCoroutine(HeadChangePosition(camUpPos));
        }
    }

    private IEnumerator HeadChangePosition(Vector3 pos)
    {
        float time = 0.4f;
        while (time > 0)
        {
            head.localPosition = Vector3.MoveTowards(head.localPosition, pos, Time.deltaTime / time);
            yield return null;
            time -= Time.deltaTime;
        }
        head.localPosition = pos;
    }
}


