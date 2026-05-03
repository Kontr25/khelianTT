using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputService : MonoBehaviour
{
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private PointerButton _leftButtonTrigger;
    [SerializeField] private PointerButton _rightButtonTrigger;
    [SerializeField] private Button _jumpButton;

    public event Action<float> OnMove; 
    public event Action OnJump; 

    private void Awake()
    {
        _jumpAction.action.performed += JumpHandle;
        _moveAction.action.performed += MoveHandle;
        _moveAction.action.canceled += MoveHandleCanceled;
        _leftButtonTrigger.OnDown += OnLeftDown;
        _rightButtonTrigger.OnDown += OnRightDown;
        _leftButtonTrigger.OnUp += OnUp;
        _rightButtonTrigger.OnUp += OnUp;
        _jumpButton.onClick.AddListener(JumpHandle);
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
        _jumpAction.action.performed -= JumpHandle;
        _moveAction.action.performed -= MoveHandle;
        _moveAction.action.canceled -= MoveHandleCanceled;
        _leftButtonTrigger.OnDown -= OnLeftDown;
        _rightButtonTrigger.OnDown -= OnRightDown;
        _leftButtonTrigger.OnUp -= OnUp;
        _rightButtonTrigger.OnUp -= OnUp;
        _jumpButton.onClick.RemoveListener(JumpHandle);
    }

    private void MoveHandle(InputAction.CallbackContext context)
    {
        float moveVal = context.ReadValue<float>();
        OnMove?.Invoke(moveVal);
    }
    private void MoveHandleCanceled(InputAction.CallbackContext context)
    {
        Debug.LogError("canceled!");
        float moveVal = context.ReadValue<float>();
        OnMove?.Invoke(moveVal);
    }

    private void OnLeftDown() => MoveHandle(-1);
    private void OnRightDown() => MoveHandle(1);
    private void OnUp() => MoveHandle(0);
    private void MoveHandle(float value) => OnMove?.Invoke(value);

    private void JumpHandle(InputAction.CallbackContext context) => OnJump?.Invoke();
    private void JumpHandle() => OnJump?.Invoke();
}
