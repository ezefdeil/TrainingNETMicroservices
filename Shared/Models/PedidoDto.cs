namespace ProductosWebApi.Models
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalAPagar { get; set; }
    }

}