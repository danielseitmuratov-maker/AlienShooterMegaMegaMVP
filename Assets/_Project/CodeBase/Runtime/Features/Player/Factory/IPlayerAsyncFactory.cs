using _Project.CodeBase.Runtime.Common.Factories;
using _Project.CodeBase.Runtime.Features.Player.Behaviours;

namespace _Project.CodeBase.Runtime.Features.Player.Factory
{
    public interface IPlayerAsyncFactory : IAsyncFactory<IPlayerFacade>
    {
    }
}