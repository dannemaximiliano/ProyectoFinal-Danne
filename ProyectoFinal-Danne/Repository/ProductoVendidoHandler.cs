using ProyectoFinal_Danne.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal_Danne.Repository
{
    internal class ProductoVendidoHandler : DbHandler
    {
        public static List<ProductoVendido> GetProductosVendidos(int id)
        {
            List<ProductoVendido> listProductosVendidos = new List<ProductoVendido>();
            List<Producto> listProductos = ProductoHandler.GetProductos(id);

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {


                    foreach (Producto producto in listProductos)
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = @"select * from ProductoVendido where IdProducto = @idProducto";
                        sqlCommand.Parameters.AddWithValue("@idProducto", producto.Id);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);
                        sqlCommand.Parameters.Clear();

                        foreach (DataRow row in table.Rows)
                        {
                            ProductoVendido ProductoVendido = new ProductoVendido();
                            ProductoVendido.Id = Convert.ToInt32(row["Id"]);
                            ProductoVendido.Stock = Convert.ToInt32(row["Stock"]);
                            ProductoVendido.IdProducto = Convert.ToInt32(row["IdProducto"]);
                            ProductoVendido.IdVenta = Convert.ToInt32(row["IdVenta"]);

                            listProductosVendidos.Add(ProductoVendido);
                        }
                        sqlCommand.Connection.Close();
                    }

                }
            }
            return listProductosVendidos;
        }
    }

}

