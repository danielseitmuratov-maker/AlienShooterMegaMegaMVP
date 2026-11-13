using System.Collections.Generic;
using System.Threading;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Assembly;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Character;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class AssemblySpaceController : IAssemblySpaceController
    {
        private readonly ICharacterAssembler _assembler;
        private readonly ICharacterDisassembler _disassembler;
        private readonly IPlayerProvider _playerProvider;
        private readonly AssemblyCharacterConfig _assemblyConfig;
        private readonly FragmentIconListView _iconList;
        private readonly GameObject _spaceRoot;

        private CharacterInstance _previewCharacter;
        private CancellationTokenSource _cts;

        public AssemblySpaceController(ICharacterAssembler assembler, ICharacterDisassembler disassembler,
            IPlayerProvider playerProvider, AssemblyCharacterConfig assemblyConfig, FragmentIconListView iconList,
            [Inject(Id = "AssemblySpaceRoot")] GameObject spaceRoot)
        {
            _assembler = assembler;
            _disassembler = disassembler;
            _playerProvider = playerProvider;
            _assemblyConfig = assemblyConfig;
            _iconList = iconList;
            _spaceRoot = spaceRoot;
        }

        public async UniTask EnterAsync(IReadOnlyList<FragmentInstance> initialFragments)
        {
            _cts = new CancellationTokenSource();
            _spaceRoot.SetActive(true);

            _previewCharacter = await _assembler.AssembleAsync(
                _assemblyConfig,
                _spaceRoot.transform.Find("SkeletonRoot"),
                System.Array.Empty<FragmentInstance>(),
                _cts.Token);

            _iconList.Populate(initialFragments);
        }

        public async UniTask ExitAsync()
        {
            if (_previewCharacter != null)
                await _disassembler.DisAssembleAsync(_previewCharacter, default);

            _previewCharacter = null;
            _iconList.Clear();

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            _spaceRoot.SetActive(false);
        }
    }
}