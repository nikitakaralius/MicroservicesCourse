using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IEventProcessor _eventProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;
        string exchange = configuration.RabbitMQExchange();
        ConnectionFactory factory = new()
        {
            HostName = configuration.RabbitMQHost(),
            Port = configuration.RabbitMQPort()
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(_queueName, exchange, "");
        Console.WriteLine("==> Listening on the Message Bus...");
        _connection.ConnectionShutdown += OnConnectionShutdown;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen) _channel.Close();
        if (_connection.IsOpen) _connection.Close();
        _connection.Dispose();
        _channel.Dispose();

        base.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        EventingBasicConsumer consumer = new(_channel);
        consumer.Received += (moduleHandle, eventArgs) =>
        {
            Console.WriteLine("==> Event Received!");
            string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            _eventProcessor.ProcessEvent(message);
        };
        _channel.BasicConsume(_queueName, true, consumer);
        return Task.CompletedTask;
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs e) =>
        Console.WriteLine("==> RabbitMQ Connection Shutdown");
}
