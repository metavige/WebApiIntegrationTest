using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WebApi.Test.Base;

public abstract class WebApplicationTestBase : IClassFixture<WebApplicationFactoryTest<Program>>
{
    private WebApplicationFactoryTest<Program> _factory;

    protected WebApplicationTestBase(WebApplicationFactoryTest<Program> factory) { _factory = factory; }

    protected HttpClient CreateClient(
        Action<IServiceCollection>? configureServices = null,
        Action<IConfigurationBuilder>? configureApp = null,
        WebApplicationFactoryClientOptions? clientOptions = null)
    { 
        return _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(configureServices ?? (services => { }));
            builder.ConfigureAppConfiguration(configureApp ?? (config => { }));
        }).CreateClient(clientOptions ?? new WebApplicationFactoryClientOptions());
    }
    
    
}

public class WebApplicationFactoryTest<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // 設定共用的服務
        builder.ConfigureServices(services => { });


    }
}