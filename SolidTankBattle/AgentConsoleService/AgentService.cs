using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AgentConsoleService;

public class AgentService
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "agent_messages";

    public AgentService()
    {
        var factory = new ConnectionFactory() { HostName = _hostname };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }



    public void Publish(string routingKey, object @event)
    {
        //string message = JsonConvert.SerializeObject(@event);
        var message = $"{JsonConvert.SerializeObject(@event)}";
        var body = Encoding.UTF8.GetBytes(message);

        var factory = new ConnectionFactory() { HostName = _hostname };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.BasicPublish(
            exchange: "",
            routingKey: _queueName,
            basicProperties: null,
            body: body);
    }
}
