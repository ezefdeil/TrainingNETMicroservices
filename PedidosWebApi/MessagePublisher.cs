using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

namespace PedidosWebApi
{
    public class MessagePublisher : IMessagePublisher
    {
        public void Publish(string message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "rabbitmquser", Password = "some_password" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", type: ExchangeType.Fanout);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);

                    Console.WriteLine($"Message Sent: {message}");

                }

            }

        }
    }
}
