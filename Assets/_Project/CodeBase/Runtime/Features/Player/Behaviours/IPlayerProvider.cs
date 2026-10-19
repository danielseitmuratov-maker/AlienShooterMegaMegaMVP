using System;

namespace _Project.CodeBase.Runtime.Features.Player.Behaviours
{
    public interface IPlayerProvider
    {
        IPlayerFacade Current { get; }
        bool HasPlayer { get; }
        event Action<IPlayerFacade> Changed;
        void Set(IPlayerFacade player);
        void Clear();
    }
}