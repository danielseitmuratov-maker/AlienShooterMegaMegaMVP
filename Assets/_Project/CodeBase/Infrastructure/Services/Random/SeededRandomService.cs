using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Random
{
    public sealed class SeededRandomService : IRandomService
    {
        private readonly System.Random _random;

        public SeededRandomService(int seed)
        {
            _random = new System.Random(seed);
        }

        public int Range(int minInclusive, int maxExclusive)
            => _random.Next(minInclusive, maxExclusive);

        public int RangeInclusive(int min, int max)
            => _random.Next(min, max + 1);

        public float Range(float minInclusive, float maxInclusive)
            => minInclusive + (float)_random.NextDouble() * (maxInclusive - minInclusive);

        public float Value => (float)_random.NextDouble();
        public float Value01 => (float)_random.NextDouble();

        public Vector2 InsideUnitCircle
        {
            get
            {
                var angle = (float)_random.NextDouble() * Mathf.PI * 2f;
                var radius = Mathf.Sqrt((float)_random.NextDouble());
                return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }
        }

        public Vector3 InsideUnitSphere
        {
            get
            {
                float u = (float)_random.NextDouble();
                float v = (float)_random.NextDouble();
                float w = (float)_random.NextDouble();

                float theta = u * Mathf.PI * 2f;
                float phi = Mathf.Acos(2f * v - 1f);
                float r = Mathf.Pow(w, 1f / 3f);

                float sinPhi = Mathf.Sin(phi);
                return new Vector3(
                    r * sinPhi * Mathf.Cos(theta),
                    r * sinPhi * Mathf.Sin(theta),
                    r * Mathf.Cos(phi));
            }
        }

        public Vector3 OnUnitSphere
        {
            get
            {
                float theta = (float)_random.NextDouble() * Mathf.PI * 2f;
                float phi = Mathf.Acos(2f * (float)_random.NextDouble() - 1f);

                float sinPhi = Mathf.Sin(phi);
                return new Vector3(
                    sinPhi * Mathf.Cos(theta),
                    sinPhi * Mathf.Sin(theta),
                    Mathf.Cos(phi));
            }
        }

        public Quaternion Rotation
        {
            get
            {
                float u1 = (float)_random.NextDouble();
                float u2 = (float)_random.NextDouble();
                float u3 = (float)_random.NextDouble();

                float sqrt1 = Mathf.Sqrt(1f - u1);
                float sqrt2 = Mathf.Sqrt(u1);
                float t1 = Mathf.PI * 2f * u2;
                float t2 = Mathf.PI * 2f * u3;

                return new Quaternion(
                    sqrt1 * Mathf.Sin(t1),
                    sqrt1 * Mathf.Cos(t1),
                    sqrt2 * Mathf.Sin(t2),
                    sqrt2 * Mathf.Cos(t2));
            }
        }

        public Quaternion RotationUniform => Rotation;

        public bool Chance(float probability) => (float)_random.NextDouble() < probability;

        public int WeightedPick(IReadOnlyList<float> weights)
        {
            if (weights == null || weights.Count == 0)
                return -1;

            float total = 0f;
            for (int i = 0; i < weights.Count; i++)
                total += weights[i];

            if (total <= 0f)
                return -1;

            float roll = (float)_random.NextDouble() * total;
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
            return _random.Next(0, count);
        }
    }
}