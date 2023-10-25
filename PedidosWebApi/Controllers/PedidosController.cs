using Microsoft.AspNetCore.Mvc;
using ProductosWebApi.Models;

namespace PedidosWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {

        private readonly ILogger<PedidosController> _logger;
        private readonly HttpClient productosClient;
        private List<PedidoDto> pedidos;

        private readonly IMessageProducer _messageProducer;
        private readonly IMessagePublisher _messagePublisher;

        public PedidosController(ILogger<PedidosController> logger, IHttpClientFactory httpClientFactory, IMessageProducer messageProducer, IMessagePublisher messagePublisher)
        {
            _logger = logger;

            productosClient = httpClientFactory.CreateClient("ProductosClient");

            pedidos = new List<PedidoDto>()
            {
                new PedidoDto{ Id = 1, ProductoId = 1, Cantidad = 4}
            };
            _messageProducer = messageProducer;
            _messagePublisher = messagePublisher;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(pedidos);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(pedidos.FirstOrDefault(x => x.Id == id));
        }


        [HttpPost("NuevoPedido")]
        public async Task<IActionResult> NuevoPedido(NuevoPedidoRequest request)
        {

            var response = await productosClient.GetAsync($"Productos/GetById/{request.ProductoId}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("El producto solicitado no existe");
            }

            var producto = await response.Content.ReadFromJsonAsync<ProductoDto>();

            if (producto == null)
            {
                return BadRequest("El producto solicitado no existe");
            }

            var pedido = new PedidoDto()
            {
                Id = pedidos.Count + 1,
                ProductoId = request.ProductoId,
                Cantidad = request.Cantidad,
                TotalAPagar = producto.PrecioUnitario * request.Cantidad
            };

            //_messageProducer.Send($"Se pidieron {pedido.Cantidad} unidades del Producto {producto.Nombre}");
            _messagePublisher.Publish($"Se pidieron {pedido.Cantidad} unidades del Producto {producto.Nombre}");

            return Ok(pedido);
        }

    }
}