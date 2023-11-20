using System.Numerics;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerConsoleService.DTO;

namespace AdapterPractice.Controller;

public class ServerControllerService : IDisposable
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "agent_messages";
    private readonly string _resultQueueName = "game_objects";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;
    private Vector2 _location;

    public ServerControllerService()
    {
        _location = new Vector2(100, 100);

        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _channel.QueueDeclare(
            queue: _resultQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _consumer = new EventingBasicConsumer(_channel);
        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Message? receivedMessage = JsonConvert.DeserializeObject<Message>(message);

            string? operation = receivedMessage?.Action;
            string? direction = receivedMessage?.Direction;

            if (operation != null
                && direction != null)
            {
                if (operation == "move")
                {
                    switch (direction)
                    {
                        case "up":
                            _location.Y -= 3;
                            break;
                        case "down":
                            _location.Y += 3;
                            break;
                        case "right":
                            _location.X += 3;
                            break;
                        case "left":
                            _location.X -= 3;
                            break;
                    }
                }

                if (operation == "shoot")
                {
                    
                }
            }

            string serializeMessage = JsonConvert.SerializeObject(receivedMessage);
            var resultMessage = Encoding.UTF8.GetBytes(serializeMessage.ToString());
            _channel.BasicPublish(
                exchange: "",
                routingKey: _resultQueueName,
                basicProperties: null,
                body: resultMessage);
        };

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: true,
            consumer: _consumer);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}