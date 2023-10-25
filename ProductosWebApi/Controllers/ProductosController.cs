using Microsoft.AspNetCore.Mvc;
using ProductosWebApi.Models;

namespace ProductosWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly ILogger<ProductosController> _logger;
        private List<Producto> productos;
        private readonly ProductoDbContext _dbContext;

        public ProductosController(ILogger<ProductosController> logger, ProductoDbContext dbContext)
        {
            _logger = logger;
            productos = new List<Producto>()
            {
                new Producto{ Id = 1, Nombre = "Teclado", PrecioUnitario = 100},
                new Producto{ Id = 2, Nombre = "Mouse", PrecioUnitario = 200},
                new Producto{ Id = 3, Nombre = "Monitor", PrecioUnitario = 400},
                new Producto{ Id = 4, Nombre = "Auriculares", PrecioUnitario = 1000},
            };
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Productos.Select(x => new ProductoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                PrecioUnitario = x.PrecioUnitario,
                Descripcion = x.Descripcion
            }).ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation($"Obtenisdasdendo informacion del producto ID: {id}");

            return Ok(_dbContext.Productos.FirstOrDefault(x => x.Id == id));
        }

    }
}