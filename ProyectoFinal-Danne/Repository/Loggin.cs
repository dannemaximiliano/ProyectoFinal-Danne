using ProyectoFinal_Danne.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal_Danne.Repository
{
    internal class Loggin : DbHandler
    {
        public Usuario IniciarSesion(String NombreUsuario, String Contraseña)
        {
            string Query = "select * from Usuario where NombreUsuario = @NombreUsuario";
            Usuario usuario = new Usuario();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = NombreUsuario });
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                usuario.Id = Convert.ToInt32(reader["Id"]);
                                usuario.Nombre = (reader["Nombre"]).ToString();
                                usuario.Apellido = (reader["Apellido"]).ToString();
                                usuario.NombreUsuario = (reader["NombreUsuario"]).ToString();
                                usuario.Contraseña = (reader["Contraseña"]).ToString();
                                usuario.Mail = (reader["Mail"]).ToString();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            if (String.Equals(usuario.Contraseña, Contraseña))
            {
                return usuario;
            }
            else
            {
                return new Usuario();
            }

        }
    }
}