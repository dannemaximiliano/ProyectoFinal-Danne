using ProyectoFinal_Danne.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal_Danne.Repository
{
    internal class ProductoHandler : DbHandler
    {

        public static List<Producto> GetProductos(int IdUsuario)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Producto WHERE IdUsuario = @IDUsuario", sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@IDUsuario", IdUsuario);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {   
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                productos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productos;
        }

        public static bool CrearProducto(Producto nuevoProducto)
        {

            String Query = "INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();

                    if (nuevoProducto.Descripciones != String.Empty)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = nuevoProducto.Descripciones });
                    }
                    else
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = String.Empty });
                    }
                    sqlCommand.Parameters.Add(new SqlParameter("Costo", SqlDbType.BigInt) { Value = nuevoProducto.Costo });
                    sqlCommand.Parameters.Add(new SqlParameter("PrecioDeVenta", SqlDbType.BigInt) { Value = nuevoProducto.PrecioVenta });
                    sqlCommand.Parameters.Add(new SqlParameter("Stock", SqlDbType.BigInt) { Value = nuevoProducto.Stock });
                    sqlCommand.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = nuevoProducto.IdUsuario });

                    sqlConnection.Close();
                }
            }
            return true;
        }

        public static bool ModificarProducto(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock, idUsuario = @idUsuario WHERE Id = @idProducto;";
                SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };
                SqlParameter descripcionesParameter = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("Costo", SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("Stock", SqlDbType.BigInt) { Value = producto.Stock };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);

                    sqlCommand.ExecuteScalar();
                }

                sqlConnection.Close();
            }
            return true;

        }

        public static bool EliminarProducto(int Id)
        {
            String Query = "DELETE FROM Producto WHERE Id = @Id ;" +
                           "DELETE FROM ProductoVendido WHERE IdProducto = @Id";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    sqlConnection.Close();
                }
            }
            return true;
        }


    }

}
