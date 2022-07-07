using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Entity;

namespace DAL
{
    public class TiquetParticularRepository
    {

        DbConnection _connection;
        public TiquetParticularRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(TiquetParticular tiquetParticular)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into TiquetParticular (Codigo, HoraEntrada, HoraSalida, EstadoTiquet, ValorExtra, Total, ValorMinimo, IdTarifa, IdVehiculo, IdEstacionamiento) " +
                    "values (@Codigo, @HoraEntrada, @HoraSalida, @EstadoTiquet, @ValorExtra, @Total , @ValorMinimo , @IdTarifa, @IdVehiculo, @IdEstacionamiento)";
                command.Parameters.Add(new SqlParameter("@Codigo", tiquetParticular.Codigo));
                command.Parameters.Add(new SqlParameter("@HoraEntrada", tiquetParticular.HoraEntrada));
                command.Parameters.Add(new SqlParameter("@HoraSalida", tiquetParticular.HoraSalida));
                command.Parameters.Add(new SqlParameter("@EstadoTiquet", tiquetParticular.EstadoTiquet));
                command.Parameters.Add(new SqlParameter("@ValorExtra", tiquetParticular.ValorExtra));
                command.Parameters.Add(new SqlParameter("@Total", tiquetParticular.ValorTotal));
                command.Parameters.Add(new SqlParameter("@ValorMinimo", tiquetParticular.ValorMinimo));
                command.Parameters.Add(new SqlParameter("@IdTarifa", tiquetParticular.Tipo));
                command.Parameters.Add(new SqlParameter("@IdVehiculo", tiquetParticular.Placa));
                command.Parameters.Add(new SqlParameter("@IdEstacionamiento", tiquetParticular.NumeroEspacio));
                int fila = command.ExecuteNonQuery();
            }
        }

        public void Modificar(TiquetParticular tiquetParticular)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update TiquetParticular set Codigo=@Codigo, HoraEntrada=@HoraEntrada, HoraSalida=@HoraSalida, EstadoTiquet=@EstadoTiquet," +
                    " ValorExtra=@ValorExtra, Total=@Total, ValorMinimo=@ValorMinimo, IdTarifa=@IdTarifa, IdVehiculo=@IdVehiculo, IdEstacionamiento=@IdEstacionamiento " +
                    "where Codigo=@Codigo";
                command.Parameters.Add(new SqlParameter("@Codigo", tiquetParticular.Codigo));
                command.Parameters.Add(new SqlParameter("@HoraEntrada", tiquetParticular.HoraEntrada));
                command.Parameters.Add(new SqlParameter("@HoraSalida", tiquetParticular.HoraSalida));
                command.Parameters.Add(new SqlParameter("@EstadoTiquet", tiquetParticular.EstadoTiquet));
                command.Parameters.Add(new SqlParameter("@ValorExtra", tiquetParticular.ValorExtra));
                command.Parameters.Add(new SqlParameter("@Total", tiquetParticular.ValorTotal));
                command.Parameters.Add(new SqlParameter("@ValorMinimo", tiquetParticular.ValorMinimo));
                command.Parameters.Add(new SqlParameter("@IdTarifa", Convert.ToInt32(tiquetParticular.Tipo)));
                command.Parameters.Add(new SqlParameter("@IdVehiculo", tiquetParticular.Placa));
                command.Parameters.Add(new SqlParameter("@IdEstacionamiento", tiquetParticular.NumeroEspacio));
                int fila = command.ExecuteNonQuery();
            }
        }

        public TiquetParticular BuscarPorCodigo(string placa)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from TiquetParticular where IdVehiculo=@IdVehiculo";
                command.Parameters.Add(new SqlParameter("@IdVehiculo", placa));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TiquetParticular tiquet = new TiquetParticular();
                        tiquet.Codigo = reader.GetString(0);
                        tiquet.HoraEntrada = reader.GetString(1);
                        tiquet.HoraSalida = reader.GetString(2);
                        tiquet.EstadoTiquet = reader.GetString(3);
                        tiquet.ValorExtra = reader.GetDecimal(4);
                        tiquet.ValorTotal = reader.GetDecimal(5);
                        tiquet.ValorMinimo = reader.GetDecimal(6);
                        tiquet.Tipo = reader.GetString(7);
                        tiquet.Placa = reader.GetString(8);
                        tiquet.NumeroEspacio = reader.GetInt32(9);
                        return tiquet;
                    }
                }
                reader.Close();
            }
            return null;
        }

        public List<TiquetParticular> Consultar()
        {
            List<TiquetParticular> tiquetParticulars = new List<TiquetParticular>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from TiquetParticular";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TiquetParticular tiquet = new TiquetParticular();

                    tiquet.Codigo = reader.GetString(0);
                    tiquet.HoraEntrada = reader.GetString(1);
                    tiquet.HoraSalida = reader.GetString(2);
                    tiquet.EstadoTiquet =reader.GetString(3);
                    tiquet.ValorExtra = reader.GetDecimal(4);
                    tiquet.ValorTotal = reader.GetDecimal(5);
                    tiquet.ValorMinimo = reader.GetDecimal(6);
                    tiquet.Tipo1 = reader.GetDecimal(7);
                    tiquet.Placa = reader.GetString(8);
                    tiquet.NumeroEspacio1 = reader.GetString(9);

                    tiquetParticulars.Add(tiquet);
                }
                reader.Close();
            }

            return tiquetParticulars;
        }

        public int TotalRegistros()
        {
            return Consultar().Count;
        }

        public TiquetParticular ConsultaPorPlaca(string placa)
        {
            foreach(TiquetParticular item in Consultar())
            {
                if (item.Placa.Equals(placa))
                {
                    return item;
                }
            }
            return null;
        }

    }
}