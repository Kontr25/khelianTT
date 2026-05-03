using DefaultNamespace;
using UnityEngine;

public class PlayerMover
{
    private PlayerGravityController _playerGravityController;
    private Rigidbody2D _rb;
    private float _jumpForce;
    private float _moveSpeed;
    private LayerMask _groundLayer;
    private float _rayDistance;

    public PlayerMover(PlayerGravityController playerGravityController, Rigidbody2D rb, PlayerSettings playerSettings)
    {
        _playerGravityController = playerGravityController;
        _rb = rb;
        _jumpForce = playerSettings.JumpForce;
        _moveSpeed = playerSettings.MoveSpeed;
        _groundLayer = playerSettings.GroundLayer;

        if (_rb.TryGetComponent(out CircleCollider2D circleCollider2D))
            _rayDistance = circleCollider2D.radius + 0.1f;
        else Debug.LogError("Не удалось установить дистанцию луча ");
    }

    public void Move(float moveInput)
    {
        Vector2 moveDirection = GetRightDirection(moveInput);
        _rb.linearVelocity = new Vector2(
            moveDirection.x * _moveSpeed,
            moveDirection.y * _moveSpeed
        );
    }

    private Vector2 GetRightDirection(float moveInput)
    {
        Vector2 gravity = _playerGravityController.CurrentGravityDirection;
        return new Vector2(-gravity.y, gravity.x) * moveInput;
    }

    public void Jump()
    {
        if (!IsGrounded()) return;
        
        Vector2 jumpDirection = -_playerGravityController.CurrentGravityDirection;
        _rb.AddForce(jumpDirection * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            _rb.position,
            _playerGravityController.CurrentGravityDirection,
            _rayDistance,
            _groundLayer
        );

        return hit.collider != null;
    }
}
