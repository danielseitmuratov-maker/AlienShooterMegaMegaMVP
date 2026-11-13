using System;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.AssemblyUi
{
    public sealed class FragmentIconView : MonoBehaviour
    {
        public event Action<FragmentInstance> Clicked;
        public FragmentInstance Fragment { get; private set; }

        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _root;

        private void Awake() =>
            _button.onClick.AddListener(OnClicked);

        public void Setup(FragmentInstance fragment)
        {
            Fragment = fragment;
            
            if (_icon != null && fragment?.DefinitionConfig != null)
                _icon.sprite = fragment.DefinitionConfig.Icon;
        }

        public void SetVisible(bool visible)
        {
            if (_root != null)
                _root.SetActive(visible);
            else 
                gameObject.SetActive(visible);
        }

        private void OnClicked()
        {
            if (Fragment.IsNotNull())
                Clicked?.Invoke(Fragment);
        }
        
        private void OnDestroy() => _button.onClick.RemoveListener(OnClicked);

    }
}