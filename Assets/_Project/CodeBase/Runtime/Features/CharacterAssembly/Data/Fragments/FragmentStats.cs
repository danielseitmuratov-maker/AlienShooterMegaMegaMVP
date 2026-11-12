using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments
{
    [Serializable]
    public struct FragmentStats : IEquatable<FragmentStats>
    {
        [Header("Offensive")]
        public float DamageBonus;            
        public float AttackSpeedBonus;    
        public float CritChanceBonus;       
        public float CritDamageBonus;      

        [Header("Defensive")]
        public float MaxHealthBonus;        
        public float ArmorBonus;             
        public float HealthRegenBonus;       

        [Header("Mobility")]
        public float MoveSpeedBonus;         
        public float JumpHeightBonus;        

        [Header("Utility")]
        public float CooldownReductionBonus;  
        public float RangeBonus;             
        public float LuckBonus;              

        [Header("Special")]
        public float FreezeDurationBonus;   
        public float HealAmountBonus;        
        public float LifestealPercent;       

        public static FragmentStats Zero => default;
        public static FragmentStats Empty => default;

        public static FragmentStats operator +(FragmentStats a, FragmentStats b)
        {
            return new FragmentStats
            {
                DamageBonus            = a.DamageBonus            + b.DamageBonus,
                AttackSpeedBonus       = a.AttackSpeedBonus       + b.AttackSpeedBonus,
                CritChanceBonus        = a.CritChanceBonus        + b.CritChanceBonus,
                CritDamageBonus        = a.CritDamageBonus        + b.CritDamageBonus,
                MaxHealthBonus         = a.MaxHealthBonus         + b.MaxHealthBonus,
                ArmorBonus             = a.ArmorBonus             + b.ArmorBonus,
                HealthRegenBonus       = a.HealthRegenBonus       + b.HealthRegenBonus,
                MoveSpeedBonus         = a.MoveSpeedBonus         + b.MoveSpeedBonus,
                JumpHeightBonus        = a.JumpHeightBonus        + b.JumpHeightBonus,
                CooldownReductionBonus = a.CooldownReductionBonus + b.CooldownReductionBonus,
                RangeBonus             = a.RangeBonus             + b.RangeBonus,
                LuckBonus              = a.LuckBonus              + b.LuckBonus,
                FreezeDurationBonus    = a.FreezeDurationBonus    + b.FreezeDurationBonus,
                HealAmountBonus        = a.HealAmountBonus        + b.HealAmountBonus,
                LifestealPercent       = a.LifestealPercent       + b.LifestealPercent,
            };
        }

        public static FragmentStats operator *(FragmentStats s, float factor)
        {
            return new FragmentStats
            {
                DamageBonus            = s.DamageBonus            * factor,
                AttackSpeedBonus       = s.AttackSpeedBonus       * factor,
                CritChanceBonus        = s.CritChanceBonus        * factor,
                CritDamageBonus        = s.CritDamageBonus        * factor,
                MaxHealthBonus         = s.MaxHealthBonus         * factor,
                ArmorBonus             = s.ArmorBonus             * factor,
                HealthRegenBonus       = s.HealthRegenBonus       * factor,
                MoveSpeedBonus         = s.MoveSpeedBonus         * factor,
                JumpHeightBonus        = s.JumpHeightBonus        * factor,
                CooldownReductionBonus = s.CooldownReductionBonus * factor,
                RangeBonus             = s.RangeBonus             * factor,
                LuckBonus              = s.LuckBonus              * factor,
                FreezeDurationBonus    = s.FreezeDurationBonus    * factor,
                HealAmountBonus        = s.HealAmountBonus        * factor,
                LifestealPercent       = s.LifestealPercent       * factor,
            };
        }

        public FragmentStats Add(FragmentStats other) => this + other;
        public FragmentStats Scale(float factor) => this * factor;

        public FragmentStats WithClampedCritChance()
        {
            var copy = this;
            copy.CritChanceBonus = Mathf.Clamp01(copy.CritChanceBonus);
            return copy;
        }

        public bool IsEmpty =>
            Mathf.Approximately(DamageBonus, 0f) &&
            Mathf.Approximately(AttackSpeedBonus, 0f) &&
            Mathf.Approximately(CritChanceBonus, 0f) &&
            Mathf.Approximately(CritDamageBonus, 0f) &&
            Mathf.Approximately(MaxHealthBonus, 0f) &&
            Mathf.Approximately(ArmorBonus, 0f) &&
            Mathf.Approximately(HealthRegenBonus, 0f) &&
            Mathf.Approximately(MoveSpeedBonus, 0f) &&
            Mathf.Approximately(JumpHeightBonus, 0f) &&
            Mathf.Approximately(CooldownReductionBonus, 0f) &&
            Mathf.Approximately(RangeBonus, 0f) &&
            Mathf.Approximately(LuckBonus, 0f) &&
            Mathf.Approximately(FreezeDurationBonus, 0f) &&
            Mathf.Approximately(HealAmountBonus, 0f) &&
            Mathf.Approximately(LifestealPercent, 0f);
        public bool Equals(FragmentStats other)
        {
            return Mathf.Approximately(DamageBonus, other.DamageBonus)
                && Mathf.Approximately(AttackSpeedBonus, other.AttackSpeedBonus)
                && Mathf.Approximately(CritChanceBonus, other.CritChanceBonus)
                && Mathf.Approximately(CritDamageBonus, other.CritDamageBonus)
                && Mathf.Approximately(MaxHealthBonus, other.MaxHealthBonus)
                && Mathf.Approximately(ArmorBonus, other.ArmorBonus)
                && Mathf.Approximately(HealthRegenBonus, other.HealthRegenBonus)
                && Mathf.Approximately(MoveSpeedBonus, other.MoveSpeedBonus)
                && Mathf.Approximately(JumpHeightBonus, other.JumpHeightBonus)
                && Mathf.Approximately(CooldownReductionBonus, other.CooldownReductionBonus)
                && Mathf.Approximately(RangeBonus, other.RangeBonus)
                && Mathf.Approximately(LuckBonus, other.LuckBonus)
                && Mathf.Approximately(FreezeDurationBonus, other.FreezeDurationBonus)
                && Mathf.Approximately(HealAmountBonus, other.HealAmountBonus)
                && Mathf.Approximately(LifestealPercent, other.LifestealPercent);
        }

        public override bool Equals(object obj) => obj is FragmentStats other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + DamageBonus.GetHashCode();
                hash = hash * 31 + AttackSpeedBonus.GetHashCode();
                hash = hash * 31 + CritChanceBonus.GetHashCode();
                hash = hash * 31 + CritDamageBonus.GetHashCode();
                hash = hash * 31 + MaxHealthBonus.GetHashCode();
                hash = hash * 31 + ArmorBonus.GetHashCode();
                hash = hash * 31 + HealthRegenBonus.GetHashCode();
                hash = hash * 31 + MoveSpeedBonus.GetHashCode();
                hash = hash * 31 + JumpHeightBonus.GetHashCode();
                hash = hash * 31 + CooldownReductionBonus.GetHashCode();
                hash = hash * 31 + RangeBonus.GetHashCode();
                hash = hash * 31 + LuckBonus.GetHashCode();
                hash = hash * 31 + FreezeDurationBonus.GetHashCode();
                hash = hash * 31 + HealAmountBonus.GetHashCode();
                hash = hash * 31 + LifestealPercent.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            if (IsEmpty) return "FragmentStats(empty)";

            var sb = new System.Text.StringBuilder("FragmentStats(");
            AppendIfNonZero(sb, "DMG", DamageBonus);
            AppendIfNonZero(sb, "AS", AttackSpeedBonus);
            AppendIfNonZero(sb, "CRIT", CritChanceBonus);
            AppendIfNonZero(sb, "CD", CritDamageBonus);
            AppendIfNonZero(sb, "HP", MaxHealthBonus);
            AppendIfNonZero(sb, "ARM", ArmorBonus);
            AppendIfNonZero(sb, "REGEN", HealthRegenBonus);
            AppendIfNonZero(sb, "SPD", MoveSpeedBonus);
            AppendIfNonZero(sb, "JUMP", JumpHeightBonus);
            AppendIfNonZero(sb, "CDR", CooldownReductionBonus);
            AppendIfNonZero(sb, "RNG", RangeBonus);
            AppendIfNonZero(sb, "LUCK", LuckBonus);
            AppendIfNonZero(sb, "FREEZE", FreezeDurationBonus);
            AppendIfNonZero(sb, "HEAL", HealAmountBonus);
            AppendIfNonZero(sb, "LS", LifestealPercent);
            sb.Append(")");
            return sb.ToString();
        }

        private static void AppendIfNonZero(System.Text.StringBuilder sb, string name, float value)
        {
            if (Mathf.Approximately(value, 0f)) return;
            if (sb.Length > "FragmentStats(".Length) sb.Append(", ");
            sb.Append(name).Append('=').Append(value.ToString("F2"));
        }
    }
}