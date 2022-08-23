using ProyectoFinal_Danne.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal_Danne.Repository
{
    internal class VentaHandler : DbHandler
    {
        public static List<Venta> GetVentas(int id)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = @"SELECT * FROM Venta WHERE IdUsuario = @idUsuario;";
                    sqlCommand.Parameters.AddWithValue("@idUsuario", id);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(row["Id"]);
                        venta.Comentarios = row["Comentarios"].ToString();
                        venta.IdUsuario = Convert.ToInt32(row["IdUsuario"]);
                        ventas.Add(venta);
                    }
                    sqlCommand.Connection.Close();
                }
            }
            return ventas;
        }


    }

}
