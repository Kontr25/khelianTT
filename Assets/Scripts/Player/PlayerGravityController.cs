using UnityEngine;

namespace Player
{
    public class PlayerGravityController : MonoBehaviour
    {
        [SerializeField] private Collider2D _targetPlatform;
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private float _gravityPower = 9.81f;

        private Vector2 _currentGravityDirection;
        public Vector2 CurrentGravityDirection => _currentGravityDirection;

        public void RefreshDirection()
        {
            Vector2 closestPoint = _targetPlatform.ClosestPoint(_playerRigidbody.position);
            Vector2 toPlatform = closestPoint - _playerRigidbody.position;
            if (toPlatform.sqrMagnitude <= 0)
                return;

            _currentGravityDirection = toPlatform.normalized;
        }

        public void ApplyLocalGravityForce()
        {
            Vector2 worldDown = (Vector2)_playerRigidbody.transform.TransformDirection(new Vector3(0f, -1f, 0f));
            if (worldDown.sqrMagnitude <= 0)
                worldDown = _currentGravityDirection;
            _playerRigidbody.AddForce(worldDown.normalized * (_gravityPower * _playerRigidbody.mass));
        }
    }
}