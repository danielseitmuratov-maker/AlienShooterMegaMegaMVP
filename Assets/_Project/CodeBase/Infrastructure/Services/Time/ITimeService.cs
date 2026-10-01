using System;

namespace _Project.CodeBase.Infrastructure.Services.Time
{
    public interface ITimeService
    {
        public float DeltaTime { get; }
        public float InGameTime { get; }

        public DateTime UtcNow { get; }
    }
}