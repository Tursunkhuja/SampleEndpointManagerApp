using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public static class GenericHostBuilderExtensions
{
    /// <summary>
    /// Configures a <see cref="IHostBuilder" /> with defaults for hosting a web app. This should be called
    /// before application specific configuration to avoid it overwriting provided services, configuration sources,
    /// environments, content root, etc.
    /// </summary>
    /// <remarks>
    /// The following defaults are applied to the <see cref="IHostBuilder"/>:
    /// <list type="bullet">
    ///     <item><description>use Kestrel as the web server and configure it using the application's configuration providers</description></item>
    ///     <item><description>configure <see cref="IWebHostEnvironment.WebRootFileProvider"/> to include static web assets from projects referenced by the entry assembly during development</description></item>
    ///     <item><description>adds the HostFiltering middleware</description></item>
    ///     <item><description>adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true,</description></item>
    ///     <item><description>enable IIS integration</description></item>
    ///   </list>
    /// </remarks>
    /// <param name="builder">The <see cref="IHostBuilder" /> instance to configure.</param>
    /// <param name="configure">The configure callback</param>
    /// <returns>A reference to the <paramref name="builder"/> after the operation has completed.</returns>
    public static IHostBuilder ConfigureWebHostDefaults(this IHostBuilder builder, Action<IWebHostBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return builder.ConfigureWebHost(webHostBuilder =>
        {
            //WebHost.ConfigureWebDefaults(webHostBuilder);

            configure(webHostBuilder);
        });
    }


    public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return builder.ConfigureWebHost(configure, _ => { });
    }

    /// <summary>
    /// Adds and configures an ASP.NET Core web application.
    /// </summary>
    /// <param name="builder">The <see cref="IHostBuilder"/> to add the <see cref="IWebHostBuilder"/> to.</param>
    /// <param name="configure">The delegate that configures the <see cref="IWebHostBuilder"/>.</param>
    /// <param name="configureWebHostBuilder">The delegate that configures the <see cref="WebHostBuilderOptions"/>.</param>
    /// <returns>The <see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure, Action<WebHostBuilderOptions> configureWebHostBuilder)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        if (configureWebHostBuilder is null)
        {
            throw new ArgumentNullException(nameof(configureWebHostBuilder));
        }

        // Light up custom implementations namely ConfigureHostBuilder which throws.
        if (builder is ISupportsConfigureWebHost supportsConfigureWebHost)
        {
            return supportsConfigureWebHost.ConfigureWebHost(configure, configureWebHostBuilder);
        }

        var webHostBuilderOptions = new WebHostBuilderOptions();
        configureWebHostBuilder(webHostBuilderOptions);
        var webhostBuilder = new GenericWebHostBuilder(builder, webHostBuilderOptions);
        configure(webhostBuilder);
        builder.ConfigureServices((context, services) => services.AddHostedService<GenericWebHostService>());
        return builder;
    }
}