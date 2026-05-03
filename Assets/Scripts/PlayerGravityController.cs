using UnityEngine;

public class PlayerGravityController : MonoBehaviour
{
    [SerializeField] private Collider2D _targetPlatform;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private float _gravityPower = 9.81f;

    private Vector2 _currentGravityDirection;
    public Vector2 CurrentGravityDirection => _currentGravityDirection;

    void FixedUpdate()
    {
        Vector2 closestPoint = _targetPlatform.ClosestPoint(_playerRigidbody.position);
        _currentGravityDirection = (closestPoint - _playerRigidbody.position).normalized;
        _playerRigidbody.AddForce(_currentGravityDirection * _gravityPower);
    }
}