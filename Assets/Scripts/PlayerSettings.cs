using UnityEngine;

[CreateAssetMenu (fileName = nameof(PlayerSettings), menuName = "Player/" + nameof(PlayerSettings))]
public class PlayerSettings: ScriptableObject
{
    public LayerMask GroundLayer;
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
}
