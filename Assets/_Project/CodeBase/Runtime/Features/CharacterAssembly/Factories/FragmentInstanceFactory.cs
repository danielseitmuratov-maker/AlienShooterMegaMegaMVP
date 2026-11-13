using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Factories
{
    public sealed class FragmentInstanceFactory : IFragmentInstanceFactory
    {
        public FragmentInstance Create(FragmentDefinitionConfig config)
        {
            if (config == null)
            {
                Debug.LogError("[FragmentInstanceFactory] Config is null.");
                return null;
            }

            return new FragmentInstance(Guid.NewGuid(), config, DateTime.UtcNow);
        }
    }
}