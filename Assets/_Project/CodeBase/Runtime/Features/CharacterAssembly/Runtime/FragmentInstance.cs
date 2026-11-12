using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime
{
    public sealed class FragmentInstance
    {
        public Guid InstanceId { get; }
        public FragmentDefinitionConfig DefinitionConfig { get; }
        public DateTime CreatedAt { get; }
        
        public Guid? OwnerId { get; private set; }
        public bool IsEquipped { get; private set; }
        public float Durability { get; private set; }
        public FragmentStats CustomModifiers { get; private set; }

        public float MaxDurability => 
            DefinitionConfig != null ? DefinitionConfig.MaxDurability : 0f;
        public bool IsBroken =>
            Durability <= 0f;
        public bool IsOwned => 
            OwnerId.HasValue;

        public FragmentInstance(Guid instanceId, FragmentDefinitionConfig definitionConfig, DateTime createdAt)
        {
            if (definitionConfig == null)
                throw new ArgumentNullException(nameof(definitionConfig));

            InstanceId = instanceId;
            DefinitionConfig = definitionConfig;
            CreatedAt = createdAt;
            
            Durability = definitionConfig.MaxDurability;
            CustomModifiers = default;
        }
        
        public FragmentInstance Create(FragmentDefinitionConfig definitionConfig) => 
            new FragmentInstance(Guid.NewGuid(), definitionConfig, DateTime.UtcNow);


        public void SetOwner(Guid ownerId) => 
            OwnerId = ownerId;

        public void ClearOwner()
        {
            OwnerId = null;
            IsEquipped = false;
        }
        
        
        public void MarkEquipped()
        {
            if (!IsOwned)
                throw new InvalidOperationException(
                    "Cannot equip a fragment without an owner.");

            IsEquipped = true;
        }

        public void MarkUnequipped() => 
            IsEquipped = false;
        
        
        public void ApplyWear(float amount)
        {
            if (amount <= 0f) return;
            Durability = Math.Max(0f, Durability - amount);
        }

        public void Repair(float amount)
        {
            if (amount <= 0f) return;
            Durability = Math.Min(MaxDurability, Durability + amount);
        }

        public void RestoreFullDurability() => 
            Durability = MaxDurability;


        public void ApplyModifiers(FragmentStats modifiers) => 
            CustomModifiers = CustomModifiers.Add(modifiers);

        public void ResetModifiers() => 
            CustomModifiers = default;
        
        public FragmentStats GetTotalStats()
        {
            var baseStats = DefinitionConfig.BaseStats;
            var total = baseStats.Add(CustomModifiers);
            
            if (MaxDurability > 0f)
            {
                float durabilityFactor = Durability / MaxDurability;
                total = total.Scale(durabilityFactor);
            }

            return total;
        }
        
        public bool IsCompatibleWith(SlotType slotType)
        {
            var compatible = DefinitionConfig.CompatibleSlots;
            if (compatible == null) return false;

            for (int i = 0; i < compatible.Count; i++)
            {
                if (compatible[i] == slotType) return true;
            }
            return false;
        }

        public override bool Equals(object obj) => 
            obj is FragmentInstance other && InstanceId.Equals(other.InstanceId);

        public override int GetHashCode() => 
            InstanceId.GetHashCode();

        public override string ToString()
        {
            var defName = DefinitionConfig != null ? DefinitionConfig.name : "<null>";
            return $"Fragment[{defName}]({InstanceId:N}, durability={Durability:F1}/{MaxDurability:F1})";
        }
    }
}