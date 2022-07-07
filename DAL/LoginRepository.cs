using ENTITY;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginRepository
    {
        DbConnection _connection;
        public LoginRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Guardar(Login login)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "insert into Login (Usuario, Contraseña ) values (@Usuario, @Contraseña)";
                command.Parameters.Add(new SqlParameter("@Usuario", login.Usuario));
                command.Parameters.Add(new SqlParameter("@Contraseña", login.Contraseña));
                int fila = command.ExecuteNonQuery();

            }
        }

        public List<Login> Consultar()
        {
            List<Login> parqueaderos = new List<Login>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Login";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Login login = new Login();
                    login.Usuario = Convert.ToString(reader["Usuario"]);
                    login.Contraseña = Convert.ToString(reader["Contraseña"]);
                    parqueaderos.Add(login);
                }
                reader.Close();
            }

            return parqueaderos;
        }

        public Login VerificarLogin(string usuario)
        {
            foreach(Login login in Consultar())
            {
                if (login.Usuario.Equals(usuario))
                {
                    return login;
                }
            }
            return null;
        }

        public void Modificar(Login login)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update Login set Contraseña=@Contraseña where Usuario=@Usuario";
                command.Parameters.Add(new SqlParameter("@Usuario", login.Usuario));
                command.Parameters.Add(new SqlParameter("@Contraseña", login.Contraseña));
                int fila = command.ExecuteNonQuery();

            }
        }

    }
}
