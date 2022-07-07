using Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class EstacionamientoRepository
    {
        DbConnection _connection;
        public EstacionamientoRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(Estacionamiento estacionamiento)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into Estacionamiento (NumeroEspacio, NumerodePiso,Estado ) values (@NumeroEspacio, @NumerodePiso,@Estado)";
                command.Parameters.Add(new SqlParameter("@NumeroEspacio", estacionamiento.NumeroEspacio));
                command.Parameters.Add(new SqlParameter("@NumerodePiso", estacionamiento.NumeroPiso));
                command.Parameters.Add(new SqlParameter("@Estado", estacionamiento.Estado));
                int fila = command.ExecuteNonQuery();

            }
        }

        public List<Estacionamiento> Consultar()
        {
            List<Estacionamiento> estacionamientos = new List<Estacionamiento>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Estacionamiento";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estacionamiento estacionamiento = new Estacionamiento();
                    estacionamiento.NumeroEspacio = Convert.ToInt32(reader["NumeroEspacio"]);
                    estacionamiento.NumeroPiso = Convert.ToInt32(reader["NumerodePiso"]);
                    estacionamiento.Estado = Convert.ToString((reader["Estado"]));
                    estacionamientos.Add(estacionamiento);
                }
                reader.Close();
            }

            return estacionamientos;
        }

        public void Modificar(Estacionamiento estacionamiento)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update Estacionamiento set NumeroEspacio=@NumeroEspacio, NumerodePiso=@NumerodePiso," +
                    " Estado=@Estado where NumeroEspacio=@NumeroEspacio";
                command.Parameters.Add(new SqlParameter("@NumeroEspacio", estacionamiento.NumeroEspacio));
                command.Parameters.Add(new SqlParameter("@NumerodePiso", estacionamiento.NumeroPiso));
                command.Parameters.Add(new SqlParameter("@Estado", estacionamiento.Estado));
                int fila = command.ExecuteNonQuery();

            }
        }
    }
}