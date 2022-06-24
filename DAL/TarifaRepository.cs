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
        public Tarifa tarifa = new Tarifa();
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

        public Tarifa BuscarCodigo(string tipo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select MAX(Fecha) from Tarifa where TipoVehiculo = @Tipo";
                command.Parameters.Add(new SqlParameter("@Tipo", tipo));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tarifa tarifa = new Tarifa();
                        tarifa.TipoVehiculo = Convert.ToInt32(reader["TipoVehiculo"]);
                        return tarifa;
                    }
                }
                reader.Close();
            }
            return null;
        }

        public void Modificar(Tarifa tarifa, string tipoVehiculo)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update persona set TipoVehiculo=@TipoVehiculo, NombreTipodeVehiculo=@NombreTipodeVehiculo,Fecha=@Fecha, ValorMinimo=@ValorMinimo,ValorNormal=@ValorNormal,TiempoMinimo=@TiempoMinimo where TipoVehiculo=@TipoVehiculo";
                command.Parameters.Add(new SqlParameter("@TipoVehiculo", tarifa.TipoVehiculo));
                command.Parameters.Add(new SqlParameter("@NombreTipodeVehiculo", tarifa.NombreTipodeVehiculo));
                command.Parameters.Add(new SqlParameter("@Fecha", tarifa.Fecha));
                command.Parameters.Add(new SqlParameter("@ValorMinimo", tarifa.ValorMinimo));
                command.Parameters.Add(new SqlParameter("@ValorNormal", tarifa.ValorNormal));
                command.Parameters.Add(new SqlParameter("@TiempoMinimo", tarifa.TiempoMinimo));
                int fila = command.ExecuteNonQuery();

            }
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
                        tarifa.TipoVehiculo = reader.GetInt32(0);
                        tarifa.NombreTipodeVehiculo = reader.GetString(1);
                        tarifa.Fecha = reader.GetDateTime(2);
                        tarifa.ValorMinimo = reader.GetInt32(3);
                        tarifa.ValorNormal = reader.GetInt32(3);
                        tarifa.TiempoMinimo = reader.GetInt32(3);
                        return tarifa;
                    }
                }

                reader.Close();
            }
            return null;
        }

        public List<Tarifa> FiltrarPorTipoVehiculo(string tipo)
        {
            return (from p in Consultar()
                    where p.TipoVehiculo.Equals(tipo)
                    orderby p.NombreTipodeVehiculo ascending
                    select p).ToList();
        }

        public List<Tarifa> FiltrarPorPalabraTipoVehiculo(string palabra)
        {
            return (from p in Consultar()
                    where p.NombreTipodeVehiculo.ToLower().Contains(palabra.ToLower())
                    select p).ToList();
        }

    }
}