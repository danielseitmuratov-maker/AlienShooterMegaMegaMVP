using System.Collections.Generic;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Factories;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public sealed class StartingFragmentsProvider : IStartingFragmentsProvider
    {
        private readonly StartingFragmentsConfig _config;
        private readonly IFragmentInstanceFactory _factory;

        public StartingFragmentsProvider(StartingFragmentsConfig config, IFragmentInstanceFactory factory)
        {
            _config = config;
            _factory = factory;
        }

        public IReadOnlyList<FragmentInstance> GetStartingFragments()
        {
            List<FragmentInstance> result = new List<FragmentInstance>(_config.Fragments.Count);

            for (int i = 0; i < _config.Fragments.Count; i++)
            {
                FragmentDefinitionConfig definitionConfig = _config.Fragments[i];
                if (definitionConfig == null) 
                    continue;

                FragmentInstance instance = _factory.Create(definitionConfig);
                if (instance != null)
                    result.Add(instance);
            }

            return result;
        }
    }
}