using UnityEngine;

namespace _Project.CodeBase.Configs.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "SO/" + nameof(PlayerMovementConfig), order = 0)]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxMoveSped { get; private set; }
    }
}