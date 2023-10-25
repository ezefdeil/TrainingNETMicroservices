using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

namespace PedidosWebApi
{
    public class MessageProducer : IMessageProducer
    {
        public void Send(string message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "rabbitmquser", Password = "some_password" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "producto", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "producto", basicProperties: null, body: body);

                    Console.WriteLine($"Message Sent: {message}");

                }

            }

        }
    }
}
