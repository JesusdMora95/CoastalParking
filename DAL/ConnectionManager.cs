using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{

    public class ConnectionManager
    {
        public DbConnection Connection;

        public ConnectionManager(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            Connection.Open();
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}