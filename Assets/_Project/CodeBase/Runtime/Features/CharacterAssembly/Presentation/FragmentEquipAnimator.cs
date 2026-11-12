using System;
using _Project.CodeBase.Runtime.Features.CharacterAssembly.Events;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Features.CharacterAssembly.Presentation
{
    public sealed class FragmentEquipAnimator : IInitializable, IDisposable
    {
        private readonly ICharacterAssemblyEvents _events;
        private readonly EquipAnimationConfigsConfig _config;

        public FragmentEquipAnimator(ICharacterAssemblyEvents events, EquipAnimationConfigsConfig config)
        {
            _events = events;
            _config = config;
        }

        public void Initialize() => 
            SubscribeToEvents();

        private void SubscribeToEvents() => 
            _events.FragmentEquipped += OnFragmentEquipped;

        private void UnsubscribeFromEvents() => 
            _events.FragmentEquipped -= OnFragmentEquipped;
        

        private void OnFragmentEquipped(FragmentEquippedArgs args)
        {
            GameObject visual = args.Visual;
            
            if (visual == null) 
                return;

            EquipAnimationConfig config = _config.Get(args.Fragment.DefinitionConfig.Type);
            
            if (config == null)
            {
                Debug.LogWarning(
                    $"[FragmentEquipAnimator] No animation config for " +
                    $"{args.Fragment.DefinitionConfig.Type}.");
                return;
            }

            PlaySimpleAnimation(visual, config);
        }

        private void PlaySimpleAnimation(GameObject visual, EquipAnimationConfig config)
        {
            Transform transform = visual.transform;
            transform.DOKill();
            transform.localScale = config.StartScale;
            transform.DOScale(Vector3.one, config.Duration)
                .SetEase(config.Ease)
                .SetDelay(config.Delay);
        }

        public void Dispose() => 
            UnsubscribeFromEvents();
    }
}