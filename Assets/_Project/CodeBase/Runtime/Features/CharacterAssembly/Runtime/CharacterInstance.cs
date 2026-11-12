using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime
{
    public sealed class CharacterInstance
    {
        public AssemblyCharacterConfig Config { get; }
        public Transform SkeletonRoot { get; }
        public IReadOnlyDictionary<string, FragmentInstance> Equipment => _equipment;
        public IReadOnlyDictionary<string, GameObject> Visuals => _visuals;
        public IReadOnlyDictionary<string, Transform> Bones => _bones;

        private readonly Dictionary<string, FragmentInstance> _equipment = new Dictionary<string, FragmentInstance>();
        private readonly Dictionary<string, GameObject> _visuals = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, Transform> _bones = new Dictionary<string, Transform>();

        public CharacterInstance(AssemblyCharacterConfig config, Transform skeletonRoot)
        {
            Config = config;
            SkeletonRoot = skeletonRoot;

            BuildBoneLookup(skeletonRoot);
        }

        public bool IsSlotOccupied(string slotId) => _equipment.ContainsKey(slotId);

        public bool TryGetVisual(string slotId, out GameObject visual) =>
            _visuals.TryGetValue(slotId, out visual);

        public void AddEquipment(string slotId, FragmentInstance fragment, GameObject visual)
        {
            _equipment[slotId] = fragment;
            _visuals[slotId] = visual;
        }

        public bool TryRemoveEquipment(string slotId, out FragmentInstance fragment, out GameObject visual)
        {
            fragment = null;
            visual = null;

            if (!_equipment.Remove(slotId))
                return false;

            _visuals.Remove(slotId, out visual);
            return true;
        }

        public void Clear()
        {
            _equipment.Clear();
            _visuals.Clear();
        }

        private void BuildBoneLookup(Transform skeleton)
        {
            if (skeleton == null)
                return;

            Transform[] all = skeleton.GetComponentsInChildren<Transform>(true);
            
            for (int i = 0; i < all.Length; i++)
                if (!_bones.ContainsKey(all[i].name))
                    _bones.Add(all[i].name, all[i]);
        }
    }
}