namespace WebApi.Services;

public interface IHelloService
{
    string GetHello();
}

public class HelloService : IHelloService
{
    public string GetHello()
    {
        return "Hello World";
    }
}