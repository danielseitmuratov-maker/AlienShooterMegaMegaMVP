using System;
using System.Threading;
using _Project.CodeBase.Runtime.Common.Extensions;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Generations;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Placement;
using _Project.CodeBase.Runtime.Features.Dungeon.Locations.Streaming;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Features.Dungeon.Locations
{
    public sealed class LocationController : IInitializable, IDisposable, ITickable
    {
        private readonly IDungeonGenerator _generator;
        private readonly IDungeonStreamer _streamer;
        private readonly IPlayerDungeonPlacement _placement;
        private readonly IPlayerProvider _playerProvider;

        private Transform _root;
        private CancellationTokenSource _cts;
        private bool _isGenerating;

        public LocationController(IDungeonGenerator generator, IDungeonStreamer streamer,
            IPlayerDungeonPlacement placement, IPlayerProvider playerProvider)
        {
            _generator = generator;
            _streamer = streamer;
            _placement = placement;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            _root = new GameObject("[Dungeon]").transform;

            SubscribeToEvents();

            if (_playerProvider.HasPlayer)
                OnPlayerChanged(_playerProvider.Current);
        }

        private void SubscribeToEvents()
        {
            if (_playerProvider.IsNotNull())
                _playerProvider.Changed += OnPlayerChanged;
        }

        private void UnsubscribeFromEvents()
        {
            if (_playerProvider.IsNotNull())
                _playerProvider.Changed -= OnPlayerChanged;
        }

        public void Tick()
        {
            if (!_playerProvider.HasPlayer)
                return;

            _streamer.Tick(_playerProvider.Current.Transform.position);
        }

        private void OnPlayerChanged(IPlayerFacade player)
        {
            ResetCancellation();

            if (player == null)
            {
                _streamer.Clear();
                return;
            }

            GenerateAsync(_cts.Token).Forget(Debug.LogException);
        }


        private async UniTask GenerateAsync(CancellationToken token)
        {
            if (_isGenerating)
            {
                Debug.LogWarning("[LocationController] Generation already in progress.");
                return;
            }

            _isGenerating = true;

            try
            {
                DungeonGenerationResult result = await _generator.GenerateAsync(token);
                token.ThrowIfCancellationRequested();
                
                if (result == null)
                    return;

                _streamer.SetRooms(result.Rooms);

                await _placement.PlaceAsync(_playerProvider.Current, result.StartRoom);
                token.ThrowIfCancellationRequested();

                _streamer.Warmup(result.StartRoom.Center);

                Debug.Log(
                    $"[LocationController] Dungeon generated: seed={result.Layout.Seed}, " +
                    $"rooms={result.Rooms.Count}, start={result.StartRoom.RoomId}");
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                _isGenerating = false;
            }
        }

        private void ResetCancellation()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            _streamer.Clear();

            if (_root != null)
                UnityEngine.Object.Destroy(_root.gameObject);
        }
    }
}