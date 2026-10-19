using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Random
{
    public sealed class UnityRandomService : IRandomService
    {
        public int Range(int minInclusive, int maxExclusive)
            => UnityEngine.Random.Range(minInclusive, maxExclusive);

        public int RangeInclusive(int min, int max)
            => UnityEngine.Random.Range(min, max + 1);

        public float Range(float minInclusive, float maxInclusive)
            => UnityEngine.Random.Range(minInclusive, maxInclusive);

        public float Value => UnityEngine.Random.value;
        public float Value01 => UnityEngine.Random.value;

        public Vector2 InsideUnitCircle => UnityEngine.Random.insideUnitCircle;
        public Vector3 InsideUnitSphere => UnityEngine.Random.insideUnitSphere;
        public Vector3 OnUnitSphere => UnityEngine.Random.onUnitSphere;

        public Quaternion Rotation => UnityEngine.Random.rotation;
        public Quaternion RotationUniform => UnityEngine.Random.rotationUniform;

        public bool Chance(float probability)
            => UnityEngine.Random.value < probability;

        public int WeightedPick(IReadOnlyList<float> weights)
        {
            if (weights == null || weights.Count == 0)
                return -1;

            float total = 0f;
            for (int i = 0; i < weights.Count; i++)
                total += weights[i];

            if (total <= 0f)
                return -1;

            float roll = UnityEngine.Random.value * total;
            float cumulative = 0f;

            for (int i = 0; i < weights.Count; i++)
            {
                cumulative += weights[i];
                if (roll < cumulative)
                    return i;
            }

            return weights.Count - 1;
        }

        public int Next(int count)
        {
            if (count <= 0) return 0;
            return UnityEngine.Random.Range(0, count);
        }
    }
}