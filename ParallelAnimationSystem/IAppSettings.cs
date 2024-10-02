namespace ParallelAnimationSystem;

public interface IAppSettings
{
    int SwapInterval { get; }
    int WorkerCount { get; }
    ulong Seed { get; }
    float Speed { get; }
    bool EnableTextRendering { get; }
}