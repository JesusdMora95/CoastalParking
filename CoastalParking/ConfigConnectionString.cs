using System.Configuration;

namespace CoastalParking
{
    public static class ConfigConnectionString
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}