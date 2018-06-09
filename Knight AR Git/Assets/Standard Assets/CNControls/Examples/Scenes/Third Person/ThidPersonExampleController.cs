using UnityEngine;
using CnControls;

[RequireComponent(typeof(CharacterController))]
public class ThidPersonExampleController : MonoBehaviour
{
    public float MovementSpeed = 10f;
    [HideInInspector]
    public float inputMoveVector;

    private Transform _transform;
    private CharacterController _characterController;
    bool moving = false;

    protected virtual void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    public virtual void Update()
    {
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        Vector3 movementVector = inputVector;

        moving = inputVector != Vector3.zero ? true : false;

        if (moving)
        {
            var angle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, angle, 0);

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
