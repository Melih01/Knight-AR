using UnityEngine;
using CnControls;

// This is merely an example, it's for an example purpose only
// Your game WILL require a custom controller scripts, there's just no generic character control systems, 
// they at least depend on the animations

[RequireComponent(typeof(CharacterController))]
public class ThidPersonExampleController : MonoBehaviour
{
    public float MovementSpeed = 10f;
    [HideInInspector]
    public float inputMoveVector;


    //public Transform _mainCameraTransform;
    private Transform _transform;
    private CharacterController _characterController;
    bool moving = false;

    private void OnEnable()
    {
        //_mainCameraTransform = Camera.main.GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    public virtual void Update()
    {
        // Just use CnInputManager. instead of Input. and you're good to go
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        Vector3 movementVector = inputVector;

        moving = inputVector != Vector3.zero ? true : false;

        if (moving)
        {
            var angle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, angle, 0);

            // If we have some input
            //if (inputVector.sqrMagnitude > 0.001f)
            //{
            //    movementVector = _mainCameraTransform.TransformDirection(inputVector);
            //movementVector.y = 0f;
            //movementVector.Normalize();
            //_transform.forward = movementVector;
            //}

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * Time.deltaTime * MovementSpeed * inputMoveVector);
            inputMoveVector = inputVector.magnitude;
        }
        else
        {
            inputMoveVector = Vector3.zero.magnitude;
        }
    }
}
