﻿namespace ProductosWebApi.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Descripcion { get; set; }

    }
}
