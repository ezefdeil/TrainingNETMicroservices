using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProductosWebApi
{
    public class MessageConsumer : IMessageConsumer

    {

        private readonly IConnection _connection;
        private readonly EventingBasicConsumer _consumer;
        private IModel _channel;
        private IServiceProvider _serviceProvider;
        public MessageConsumer(IServiceProvider serviceProvider)
        {

            var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName = "rabbitmquser", Password = "some_password" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "producto", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _consumer = new EventingBasicConsumer(_channel);
            _serviceProvider = serviceProvider;
        }



        public void StartConsumer()

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



            _channel.BasicConsume("producto", true, _consumer);



            Console.WriteLine($"Startedconsumer: Connection is Open?:{_connection.IsOpen}");







        }

    }
}
