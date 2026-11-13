using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Factories
{
    public interface IFragmentInstanceFactory
    {
        FragmentInstance Create(FragmentDefinitionConfig config);
    }
}