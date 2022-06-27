using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    private readonly IHelloService _helloService;

    public HelloController(IHelloService helloService) { _helloService = helloService; }

    [HttpGet]
    public string Get()
    {
        return _helloService.GetHello();
    }
}