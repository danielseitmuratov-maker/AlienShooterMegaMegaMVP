using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "SO/" + nameof(PlayerMovementConfig), order = 0)]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxMoveSped { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}