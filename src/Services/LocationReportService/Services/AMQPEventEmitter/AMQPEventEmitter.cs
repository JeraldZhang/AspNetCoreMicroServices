using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using LocationReporter.Models;
using System;
using LocationReporter.Services;
using LocationReporter.Events;

namespace LocationReporter.Services
{

    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger _logger;

        private readonly AMQPOptions _rabbitOptions;

        private readonly ConnectionFactory _connectionFactory;

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger,
            IOptions<AMQPOptions> amqpOptions)
        {
            this._logger = logger;
            this._rabbitOptions = amqpOptions.Value;

            _connectionFactory = new ConnectionFactory
            {
                UserName = _rabbitOptions.Username,
                Password = _rabbitOptions.Password,
                VirtualHost = _rabbitOptions.VirtualHost,
                HostName = _rabbitOptions.HostName,
                Uri = new Uri(_rabbitOptions.Uri)
            };

            logger.LogInformation("AMQP Event Emitter configured with URI {0}", _rabbitOptions.Uri);
        }
        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecorded";

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            using var conn = _connectionFactory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.QueueDeclare(
                queue: QUEUE_LOCATIONRECORDED,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            string jsonPayload = locationRecordedEvent.toJson();
            var body = Encoding.UTF8.GetBytes(jsonPayload);
            channel.BasicPublish(
                exchange: "",
                routingKey: QUEUE_LOCATIONRECORDED,
                basicProperties: null,
                body: body
            );
        }
    }
}