using _Project.CodeBase.Common.Attributes;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using UnityEngine;

namespace _Project.CodeBase.Configs.Player
{
    [CreateAssetMenu(fileName = "PlayerRootConfig", menuName = "SO/" + nameof(PlayerRootConfig), order = 0)]
    public class PlayerRootConfig : ScriptableObject
    {
        [field: Required(nameof(PlayerRootConfig) + "Prefab is null")]
        [field: SerializeField] public PlayerFacade Prefab { get; private set; }
    }
}