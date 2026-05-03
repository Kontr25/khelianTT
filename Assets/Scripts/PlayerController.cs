using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerGravityController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerGravityController playerGravityController;
    
    private float _moveInput;
    private PlayerMover _playerMover;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerGravityController = GetComponent<PlayerGravityController>();
    }

    private void Start()
    {
        _playerMover = new PlayerMover(playerGravityController, _rb, _playerSettings);
    }

    private void FixedUpdate() => _playerMover.Move(_moveInput);

    public void SetMoveInput(float value) => _moveInput = value;

    public void OnJump() => _playerMover.Jump();
}
