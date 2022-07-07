using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Entity;

namespace DAL
{
    public class TarifaRepository
    {

        DbConnection _connection;
        public TarifaRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(Tarifa tarifa)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into tarifa (TipoVehiculo, NombreTipodeVehiculo, Fecha , ValorMinimo , ValorNormal , TiempoMinimo) values (@TipoVehiculo, @NombreTipodeVehiculo, @Fecha, @ValorMinimo, @ValorNormal, @TiempoMinimo)";
                MessageBox.Show(" " + tarifa.TipoVehiculo);
                command.Parameters.Add(new SqlParameter("@TipoVehiculo", tarifa.TipoVehiculo));
                command.Parameters.Add(new SqlParameter("@NombreTipodeVehiculo", tarifa.NombreTipodeVehiculo));
                command.Parameters.Add(new SqlParameter("@Fecha", tarifa.Fecha));
                command.Parameters.Add(new SqlParameter("@ValorMinimo", tarifa.ValorMinimo));
                command.Parameters.Add(new SqlParameter("@ValorNormal", tarifa.ValorNormal));
                command.Parameters.Add(new SqlParameter("@TiempoMinimo", tarifa.TiempoMinimo));
                int fila = command.ExecuteNonQuery();

            }
        }

        public List<Tarifa> Consultar()
        {
            Tarifa tarifa = new Tarifa();
            List<Tarifa> tarifas = new List<Tarifa>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Tarifa";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tarifa = new Tarifa();
                    tarifa.TipoVehiculo = Convert.ToInt32(reader["TipoVehiculo"]);
                    tarifa.NombreTipodeVehiculo = reader.GetString(1);
                    tarifa.Fecha = Convert.ToDateTime(reader.GetString(2));
                    tarifa.ValorMinimo = Convert.ToDouble(reader["ValorMinimo"]);
                    tarifa.ValorNormal = Convert.ToDouble(reader["ValorNormal"]);
                    tarifa.TiempoMinimo = Convert.ToInt32(reader["TiempoMinimo"]);
                    tarifas.Add(tarifa);
                }
                reader.Close();
            }

            return tarifas;
        }

        public string ExistenciaVehiculo(string nombre)
        {
            string mesaje = "No Existe";
            foreach(Tarifa tarifa in Consultar())
            {
                if (tarifa.NombreTipodeVehiculo.Equals(nombre))
                {
                    mesaje = "Si Existe El Vehiculo";
                }
            }
            return mesaje;
        }

        public Tarifa BuscarPorTipoVehiculo(string tipoVehiculo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from tarifa where TipoVehiculo=@TipoVehiculo";
                command.Parameters.Add(new SqlParameter("@TipoVehiculo", tipoVehiculo));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tarifa tarifa = new Tarifa();
                        tarifa.TipoVehiculo = Convert.ToInt32(reader["TipoVehiculo"]);
                        tarifa.NombreTipodeVehiculo = reader.GetString(1);
                        tarifa.Fecha = Convert.ToDateTime(reader.GetString(2));
                        tarifa.ValorMinimo = Convert.ToDouble(reader["ValorMinimo"]);
                        tarifa.ValorNormal = Convert.ToDouble(reader["ValorNormal"]);
                        tarifa.TiempoMinimo = Convert.ToInt32(reader["TiempoMinimo"]);
                        return tarifa;
                    }
                }

                reader.Close();
            }
            return null;
        }

        public List<Tarifa> FiltrarPorNombre(string nombre)
        {
            return Consultar().Where(Tarifa => Tarifa.NombreTipodeVehiculo.Contains(nombre)).ToList();
        }

        public int TotalElementos()
        {
            return Consultar().Count();
        }

    }
}