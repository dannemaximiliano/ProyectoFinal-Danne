using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_Danne.Modelo;
using ProyectoFinal_Danne.Repository;

namespace ProyectoFinal_Danne.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos(int IdUsuario)
        {
            return ProductoHandler.GetProductos(IdUsuario);
        }

        [HttpPost(Name = "PostProducto")]
        public bool CrearProducto([FromBody] Producto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public bool ModificarProducto([FromBody] Producto producto)
        {
            try
            {
                return ProductoHandler.ModificarProducto(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete(Name = "EliminarProducto")]
        public bool EliminarProducto([FromBody] int Id)
        {
            try
            {
                return ProductoHandler.EliminarProducto(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }







    }
}
