using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_Danne.Modelo;
using ProyectoFinal_Danne.Repository;

namespace ProyectoFinal_Danne.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet]
        public List<Venta> GetVentas(int IdUsuario)
        {
            return VentaHandler.GetVentas(IdUsuario);
        }
    }
}
