namespace PedidosWebApi
{
    public interface IMessageProducer
    {
        void Send(string message);
    }
}