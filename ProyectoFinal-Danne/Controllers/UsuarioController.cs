using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_Danne.Modelo;
using ProyectoFinal_Danne.Repository;

namespace ProyectoFinal_Danne.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }


        [HttpPut]
        public bool ModificarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                return UsuarioHandler.ModificarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}