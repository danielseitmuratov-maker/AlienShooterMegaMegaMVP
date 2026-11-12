using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common.Data;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments
{
    [CreateAssetMenu(fileName = "FragmentDefinitionConfig", menuName = "SO/" + nameof(FragmentDefinitionConfig), order = 0)]
    public class FragmentDefinitionConfig : ScriptableObject
    {
        [field: Header("Identity")]
        [field: SerializeField]
        public string Id { get; private set; }

        [field: SerializeField] public string DisplayName { get; private set; }

        [field: TextArea(2, 5)]
        [field: SerializeField]
        public string Description { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Header("Classification")]
        [field: SerializeField]
        public FragmentType Type { get; private set; }

        [field: SerializeField] public Rarity Rarity { get; private set; }
        [field: SerializeField] public List<SlotType> CompatibleSlots { get; private set; } = new List<SlotType>();

        [field: Header("Visual")]
        [field: SerializeField]
        public GameObject Mesh { get; private set; }

        [field: Header("Stats")]
        [field: SerializeField]
        public FragmentStats BaseStats { get; private set; }

        [field: Header("Durability")]
        [field: Min(0f)]
        [field: SerializeField]
        public float MaxDurability { get; private set; } = 100f;

        [field: Header("Behavior")]
        [field: SerializeField]
        public FragmentBehaviourDataConfig Behavior { get; private set; }

        [field: Header("Economy")]
        [field: Min(0)]
        [field: SerializeField]
        public int SellPrice { get; private set; }

        [field: Min(0f)]
        [field: SerializeField]
        public float Weight { get; private set; }

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float DropChance { get; private set; } = 0.1f;

        [field: Header("Requirements")]
        [field: Min(0)]
        [field: SerializeField]
        public int RequiredLevel { get; private set; }

        public bool HasBehavior => Behavior != null;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                Id = name;

            if (CompatibleSlots == null)
                CompatibleSlots = new List<SlotType>();
            else
                CompatibleSlots.RemoveAll(s => s == SlotType.None);
        }
#endif

        public bool IsCompatibleWith(SlotType slotType)
        {
            if (CompatibleSlots == null)
                return false;

            for (int i = 0; i < CompatibleSlots.Count; i++)
                if (CompatibleSlots[i] == slotType)
                    return true;
            return false;
        }

        public bool CanEquip(int playerLevel) =>
            playerLevel >= RequiredLevel;

        public override string ToString() =>
            $"FragmentDefinitionConfig({Id}, {Type}, {Rarity})";
    }
}