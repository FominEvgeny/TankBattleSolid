using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Text;
using GamePlayerMonogame.DTO;
using LibraryClasses.Commands;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GamePlayerMonogame.Controller;

public class ControllerGamePlayer : IControllerGamePlayer, IDisposable
{
    // Параметры подключения RabbitMQ
    private readonly string _hostname = "localhost";
    private readonly string _resultQueueName = "game_objects";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;

    // Коллекция игровых объектов
    private readonly BlockingCollection<UObject> _objectsGame;

    public ControllerGamePlayer(BlockingCollection<UObject> objectsGame)
    {
        _objectsGame = objectsGame;

        // Инициализация входящей очереди RabbitMQ
        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _resultQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _consumer = new EventingBasicConsumer(_channel);

        // Подписка на получение сообщений из брокера и обработка очереди входящих сообщений
        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Десериализуем входящее сообщение
            Message resMessage = JsonConvert.DeserializeObject<Message>(message);

            // Вызовем команду для интерпретации приказов игровому объекту с сервера
            IoC.Resolve<ICommand>(
                "InterpretCommand", 
                _objectsGame, resMessage)
                .Execute();
        };

        _channel.BasicConsume(
            queue: _resultQueueName,
            autoAck: true,
            consumer: _consumer);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}

