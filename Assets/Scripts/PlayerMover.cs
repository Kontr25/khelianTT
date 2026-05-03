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
        Vector2 g = _playerGravityController.CurrentGravityDirection;
        Vector2 n = g.sqrMagnitude > 0 ? g.normalized : Vector2.down;

        Vector2 v = _rb.linearVelocity;
        float alongSurfaceNormal = Vector2.Dot(v, n);

        Vector2 tangent = new Vector2(-n.y, n.x);
        Vector2 tangentVel = tangent * (moveInput * _moveSpeed);

        _rb.linearVelocity = alongSurfaceNormal * n + tangentVel;
    }

    public void Jump()
    {
        if (!IsGrounded()) return;

        Vector2 n = _playerGravityController.CurrentGravityDirection.normalized;
        _rb.AddForce(-n * _jumpForce, ForceMode2D.Impulse);
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
