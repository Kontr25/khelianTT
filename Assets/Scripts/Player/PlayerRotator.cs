using UnityEngine;

namespace Player
{
    public class PlayerRotator
    {

        public void ApplyRotation(PlayerGravityController gravity, Rigidbody2D rb)
        {
            Vector2 g = gravity.CurrentGravityDirection;
            if (g.sqrMagnitude <= 0)
                return;

            Vector2 headUp = -g;
            float targetZ = Vector2.SignedAngle(Vector2.up, headUp);

            float deltaDeg = Mathf.DeltaAngle(rb.rotation, targetZ);
            float rad = deltaDeg * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            Vector2 v = rb.linearVelocity;
            rb.linearVelocity = new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);

            rb.MoveRotation(targetZ);
            rb.angularVelocity = 0f;
        }
    }
}
