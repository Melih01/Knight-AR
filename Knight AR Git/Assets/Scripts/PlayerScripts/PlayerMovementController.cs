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

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Move();
        playerController.AnimationController.SetSpeed(inputMoveSpeed);
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
                var angle = inputVector.GetAngle();
                transform.rotation = Quaternion.Euler(0, angle, 0);

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
}
