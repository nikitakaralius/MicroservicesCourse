using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchange;

    public MessageBusClient(IConfiguration configuration)
    {
        ConnectionFactory factory = new()
        {
            HostName = configuration.RabbitMQHost(),
            Port = configuration.RabbitMQPort()
        };
        _exchange = configuration.RabbitMQExchange();
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(
                exchange: _exchange,
                type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += OnConnectionShutdown;
            Console.WriteLine("==> Connected to MessageBus");
        }
        catch (Exception e)
        {
            Console.WriteLine($"==>Could not connect to the Message Bus: {e.Message}");
        }
    }

    public void PublishNewPlatform(PlatformToPublish platform)
    {
        string message = JsonSerializer.Serialize(platform);

        if (_connection.IsOpen)
        {
            Console.WriteLine("==> Sending message...");
            Send(message);
        }
        else
        {
            Console.WriteLine("==> Can not send message. RabbitMQ connection is closed");
        }
    }

    public void Dispose()
    {
        if (_channel.IsOpen) _channel.Close();
        if (_connection.IsOpen) _connection.Close();
        _connection.Dispose();
        _channel.Dispose();
    }

    private void Send(string message)
    {
        byte[] body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(_exchange, "", null, body);
        Console.WriteLine($"==> Sent {message}");
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs e) =>
        Console.WriteLine("==> RabbitMQ Connection Shutdown");
}
