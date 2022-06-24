using System;
using System.Collections.Generic;
using System.Data.Common;
using Entity;
using System.Data.SqlClient;
using ENTITY;


namespace DAL
{
    public class VehiculoRepository
    {

        DbConnection _connection;
        public VehiculoRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(Vehiculo vehiculo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into Vehiculo (Placa, Tipo, ModeloyMarca, AplicaConvenio) values (@Placa, @Tipo, @ModeloyMarca, @AplicaConvenio)";
                command.Parameters.Add(new SqlParameter("@Placa", vehiculo.Placa));
                command.Parameters.Add(new SqlParameter("@Tipo", vehiculo.Tipo));
                command.Parameters.Add(new SqlParameter("@ModeloyMarca", vehiculo.ModeloyMarca));
                command.Parameters.Add(new SqlParameter("@AplicaConvenio", vehiculo.AplicaConvenio));
                int fila = command.ExecuteNonQuery();
            }
        }

        public Vehiculo BuscarPorPlaca(string placa)
        {
            Vehiculo vehiculo;
            using (var command = _connection.CreateCommand())
            {

                command.CommandText = "select * from Vehiculo where Placa=@Placa";
                command.Parameters.Add(new SqlParameter("@Placa", placa));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehiculo = new Vehiculo();
                        vehiculo.Placa = reader.GetString(0);
                        vehiculo.Tipo = reader.GetString(1);
                        vehiculo.ModeloyMarca = reader.GetString(2);
                        vehiculo.AplicaConvenio = reader.GetString(3);
                        return vehiculo;
                    }
                }
                reader.Close();
            }

            return null;
        }



        public List<Vehiculo> Consultar()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Vehiculo";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.Placa = reader.GetString(0);
                    vehiculo.Tipo = reader.GetString(1);
                    vehiculo.ModeloyMarca = reader.GetString(2);
                    vehiculo.AplicaConvenio = reader.GetString(3);
                    vehiculos.Add(vehiculo);
                }
                reader.Close();
            }

            return vehiculos;
        }

        public void Modificar(Vehiculo vehiculo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update Vehiculo set AplicaConvenio='SI' where Placa=@Placa";
                command.Parameters.Add(new SqlParameter("@Placa", vehiculo.Placa));
                int fila = command.ExecuteNonQuery();
            }
        }
    }

}