using Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ParqueaderoRepository
    {
        DbConnection _connection;
        public ParqueaderoRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(int EspacioTotal, int EspacioDisponible)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into Parqueadero (EstacioTotal, EspacioDisponible ) values (@EstacioTotal, @EspacioDisponible)";
                command.Parameters.Add(new SqlParameter("@EstacioTotal", EspacioTotal));
                command.Parameters.Add(new SqlParameter("@EspacioDisponible", EspacioDisponible));
                int fila = command.ExecuteNonQuery();

            }
        }

        public List<Parqueadero> Consultar()
        {
            List<Parqueadero> parqueaderos = new List<Parqueadero>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Parqueadero";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Parqueadero parqueadero = new Parqueadero();
                    parqueadero.EspacioTotal = Convert.ToInt32(reader["EstacioTotal"]);
                    parqueadero.EspacioDisponible = Convert.ToInt32(reader["EspacioDisponible"]);
                    parqueaderos.Add(parqueadero);
                }
                reader.Close();
            }

            return parqueaderos;
        }

        public void Modificar(int EspacioTotal, int EspacioDisponible, int Total)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update Parqueadero set EstacioTotal=@EstacioTotal, EspacioDisponible=@EspacioDisponible where EstacioTotal=@Total";
                command.Parameters.Add(new SqlParameter("@EstacioTotal", EspacioTotal));
                command.Parameters.Add(new SqlParameter("@EspacioDisponible", EspacioDisponible));
                command.Parameters.Add(new SqlParameter("@Total", Total));
                int fila = command.ExecuteNonQuery();

            }
        }

        public int ObtenerTotalRegistros()
        {
            int valor = 0;
            foreach(Parqueadero parqueadero in Consultar())
            {
                valor = parqueadero.EspacioTotal;
            }
            return valor;
        }
    }
}
