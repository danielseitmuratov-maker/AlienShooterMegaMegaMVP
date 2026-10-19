using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Random
{
    public interface IRandomService
    {
        int Range(int minInclusive, int maxExclusive);
        int RangeInclusive(int min, int max);

        float Range(float minInclusive, float maxInclusive);
        float Value { get; }
        float Value01 { get; }

        Vector2 InsideUnitCircle { get; }
        Vector3 InsideUnitSphere { get; }
        Vector3 OnUnitSphere { get; }

        Quaternion Rotation { get; }
        Quaternion RotationUniform { get; }

        int WeightedPick(IReadOnlyList<float> weights);

        bool Chance(float probability);
        int Next(int count);
    }
}