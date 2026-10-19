using System;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public class PlayerProvider : IPlayerProvider
    {
        public IPlayerFacade Current { get; private set; }
        public bool HasPlayer => Current != null;
        public event Action<IPlayerFacade> Changed;

        public void Set(IPlayerFacade player)
        {
            Current = player;
            Changed?.Invoke(player);
        }

        public void Clear()
        {
            Current = null;
            Changed?.Invoke(null);
        }
    }
}