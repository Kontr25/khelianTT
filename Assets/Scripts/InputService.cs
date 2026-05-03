using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _moveAction;

    private void Awake()
    {
        _jumpAction.action.performed += OnJump;
        
        _moveAction.action.performed += OnMove;
        _moveAction.action.canceled += OnMove;
    }

    private void OnEnable()
    {
        _jumpAction.action.Enable();
        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        _jumpAction.action.Disable();
        _moveAction.action.Disable();
    }

    private void OnDestroy()
    {
        _jumpAction.action.performed -= OnJump;
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        float moveVal = context.ReadValue<float>();
        player.SetMoveInput(moveVal);
    }

    private void OnJump(InputAction.CallbackContext context) => player.OnJump();
}
