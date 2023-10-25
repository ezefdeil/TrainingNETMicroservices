namespace PedidosWebApi
{
    public interface IMessagePublisher
    {
        void Publish(string message);
    }
}