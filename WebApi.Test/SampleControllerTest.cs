using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using WebApi.Services;
using WebApi.Test.Base;
using Xunit;

namespace WebApi.Test;

public class SampleControllerTest : WebApplicationTestBase
{ 
    
    public SampleControllerTest(WebApplicationFactoryTest<Program> factory) : base(factory)
    {
    }
    
    
    [Theory]
    [InlineData("/api/hello")]
    [InlineData("/weatherforecast")]
    public void Test1(string uri)
    {   
        // 1. Arrange
        var client = CreateClient();
        
        // 2. Act
        var response = client.GetAsync(uri).Result;
        
        // 3. Assert
        response.EnsureSuccessStatusCode();
        
    }

    [Fact]
    public void TestHello()
    {
        var returnThis = "Hello World! Ricky!";
        
        // 1. Arrange
        var client = CreateClient(configureServices: services =>
        {
            services.RemoveAll<IHelloService>();
            var helloService = Substitute.For<IHelloService>();
            
            helloService.GetHello().Returns(returnThis);

            services.AddTransient(sp => helloService);
        });
        
        // 2. Act
        var response = client.GetAsync("/api/hello").Result;
        
        // 3. Assert
        response.EnsureSuccessStatusCode();
        response.Content.ReadAsStringAsync().Result.Should().Be(returnThis);
    }

}