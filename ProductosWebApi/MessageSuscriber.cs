using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace ProductosWebApi
{
    public class MessageSuscriber : IMessageSuscriber

    {

        private readonly IConnection _connection;
        private readonly string queueName;
        private readonly EventingBasicConsumer _consumer;
        private IModel _channel;
        private IServiceProvider _serviceProvider;
        public MessageSuscriber(IServiceProvider serviceProvider)
        {

            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "rabbitmquser", Password = "some_password" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("logs", type: ExchangeType.Fanout);

            queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");

            _consumer = new EventingBasicConsumer(_channel);
            _serviceProvider = serviceProvider;
        }



        public void Suscribe()

        {
            _consumer.Received += (model, ea) =>

            {

                var body = ea.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message Received: {message}");

                //using (var scope = _serviceProvider.CreateScope())

                //{

                //    var context = scope.ServiceProvider.GetRequiredService<ProductoDbContext>();

                //    context.Productos.Add(new Models.Producto { Nombre = message, Descripcion = "", PrecioUnitario = 1000 });

                //    context.SaveChanges();

                //}





            };



            _channel.BasicConsume(queueName, true, _consumer);



            Console.WriteLine($"Startedconsumer: Connection is Open?:{_connection.IsOpen}");







        }

    }
}
