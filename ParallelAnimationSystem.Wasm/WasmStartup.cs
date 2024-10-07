using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParallelAnimationSystem.Data;
using ParallelAnimationSystem.Rendering;
using ParallelAnimationSystem.Rendering.OpenGLES;
using ParallelAnimationSystem.Windowing;
using Uno.Extensions.Logging.WebAssembly;

namespace ParallelAnimationSystem.Wasm;

public class WasmStartup(WasmAppSettings appSettings) : IStartup
{
    public IAppSettings AppSettings { get; } = appSettings;
    
    public void ConfigureLogging(ILoggingBuilder loggingBuilder)
        => loggingBuilder.AddProvider(new WebAssemblyConsoleLoggerProvider());

    public IResourceManager? CreateResourceManager(IServiceProvider serviceProvider)
        => null;

    public IWindowManager CreateWindowManager(IServiceProvider serviceProvider)
        => new WasmWindowManager();

    public IRenderer CreateRenderer(IServiceProvider serviceProvider)
        => new Renderer(
            serviceProvider.GetRequiredService<IAppSettings>(),
            serviceProvider.GetRequiredService<IWindowManager>(),
            serviceProvider.GetRequiredService<IResourceManager>(),
            serviceProvider.GetRequiredService<ILogger<Renderer>>());

    public IMediaProvider CreateMediaProvider(IServiceProvider serviceProvider)
        => new WasmMediaProvider();
}