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
                        tiquet.HoraEntrada = reader.GetDateTime(1);
                        tiquet.HoraSalida = reader.GetDateTime(2);
                        tiquet.EstadoTiquet = Convert.ToBoolean(reader.GetString(3));
                        tiquet.ValorExtra = reader.GetDouble(4);
                        tiquet.ValorTotal = reader.GetDouble(5);
                        tiquet.ValorMinimo = reader.GetDouble(6);
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

        public TiquetParticular BuscarCodigo()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select MAX(Codigo) from TiquetParticular";
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TiquetParticular tiquetParticular = new TiquetParticular();
                        tiquetParticular.Codigo = reader.GetString(0);
                        return tiquetParticular;
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
                    tiquet.HoraEntrada = reader.GetDateTime(1);
                    tiquet.HoraSalida = reader.GetDateTime(2);
                    tiquet.EstadoTiquet = Convert.ToBoolean(reader["EstadoTiquet"]);
                    tiquet.ValorExtra = reader.GetDouble(4);
                    tiquet.ValorTotal = reader.GetDouble(5);
                    tiquet.ValorMinimo = reader.GetDouble(6);
                    tiquet.Tipo = reader.GetString(7);
                    tiquet.Placa = reader.GetString(8);
                    tiquet.NumeroEspacio = reader.GetInt32(9);

                    tiquetParticulars.Add(tiquet);
                }
                reader.Close();
            }

            return tiquetParticulars;
        }

    }
}