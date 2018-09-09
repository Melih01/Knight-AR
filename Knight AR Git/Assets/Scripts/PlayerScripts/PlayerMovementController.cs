using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : CustomMonoBehaviour
{
    public bool IsCanControl { get; set; } = true;

    float inputMoveSpeed;
    bool isWillMove = false;

    PlayerController playerController;
    Transform cameraTransform;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cameraTransform = FindObjectOfType<Camera>().transform;
    }

    void Update()
    {
        Move();
        playerController.AnimationController.SetAnimationParameter(PlayerAnimatorParameter.Speed, inputMoveSpeed);
    }

    void Move()
    {
        if (IsCanControl)
        {
            var inputVector = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
            Vector3 movementVector = inputVector;

            isWillMove = inputVector != Vector2.zero ? true : false;

            if (isWillMove)
            {
                float angle = RotateAngleWithCameraView(inputVector); //Calculate Rotation Angle
                transform.rotation = Quaternion.Euler(0, angle, 0); //Set Rotation Angle

                movementVector += Physics.gravity;

                Vector3 movePosition = movementVector * Time.deltaTime * playerController.AttributesController.speed;
                movePosition.y = 0;
                transform.Translate(movePosition);
                inputMoveSpeed = inputVector.magnitude;
            }
            else
            {
                inputMoveSpeed = Vector3.zero.magnitude;
            }
        }
        else
        {
            inputMoveSpeed = Vector3.zero.magnitude;
        }
    }

    float RotateAngleWithCameraView(Vector2 inputVector)
    {
        var cameraViewDirection = cameraTransform.TransformDirection(inputVector.x, 0, inputVector.y);
        var angle = new Vector2(cameraViewDirection.x, cameraViewDirection.z).GetAngle();
        return angle;
    }
}
