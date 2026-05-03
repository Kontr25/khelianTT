using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerGravityController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerGravityController _playerGravityController;
    [SerializeField] private InputService _inputService;
    
    private float _moveInput;
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerGravityController = GetComponent<PlayerGravityController>();
    }

    private void Start()
    {
        _playerMover = new PlayerMover(_playerGravityController, _rb, _playerSettings);
        _playerRotator = new PlayerRotator();
        _inputService.OnMove += MoveHandle;
        _inputService.OnJump += JumpHandle;
    }

    private void OnDestroy()
    {
        _inputService.OnMove -= MoveHandle;
        _inputService.OnJump -= JumpHandle;
    }

    private void FixedUpdate()
    {
        _playerGravityController.RefreshDirection();
        _playerRotator.ApplyRotation(_playerGravityController, _rb);
        _playerGravityController.ApplyLocalGravityForce();
        _playerMover.Move(_moveInput);
    }

    private void MoveHandle(float value) => _moveInput = value;
    private void JumpHandle() => _playerMover.Jump();
}
