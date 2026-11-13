using _Project.CodeBase.Runtime.Features.CharacterAssembly.Data.Fragments;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class AssemblyDraggableView : MonoBehaviour
    {
        [SerializeField] private Transform _visualRoot;

        private GameObject _spawnedVisual;

        public void Initialize(FragmentDefinitionConfig config)
        {
            if (_spawnedVisual != null)
                Destroy(_spawnedVisual);

            if (config?.Mesh == null)
            {
                Debug.LogWarning("[AssemblyDraggableView] No mesh.");
                return;
            }

            _spawnedVisual = Instantiate(config.Mesh, _visualRoot);
            _spawnedVisual.transform.localPosition = Vector3.zero;
            _spawnedVisual.transform.localRotation = Quaternion.identity;
            _spawnedVisual.transform.localScale = Vector3.one;
        }

        public void SetWorldPosition(Vector3 worldPosition) => transform.position = worldPosition;
    }
}