using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using UnityEngine;

namespace _Project.CodeBase.Configs.Player
{
    [CreateAssetMenu(fileName = "PlayerRootConfig", menuName = "SO/" + nameof(PlayerRootConfig), order = 0)]
    public class PlayerRootConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerFacade Prefab { get; private set; }
    }
}