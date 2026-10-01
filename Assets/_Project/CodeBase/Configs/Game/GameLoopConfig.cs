using UnityEngine;

namespace _Project.CodeBase.Configs.Game
{
    [CreateAssetMenu(fileName = "GameLoopConfig", menuName = "SO/" + nameof(GameLoopConfig), order = 0)]
    public class GameLoopConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 StartPlayerPosition { get; private set; }
        [field: SerializeField] public Vector3 StartPlayerRotation { get; private set; }
    }
}