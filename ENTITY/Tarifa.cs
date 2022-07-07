using System;

namespace Entity
{
    public class Tarifa
    {
        public int TipoVehiculo { get; set; }
        public string NombreTipodeVehiculo { get; set; }
        public DateTime Fecha { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorNormal { get; set; }
        public int TiempoMinimo { get; set; }
        public string Mail { get; set; }

        public Tarifa()
        {
        }
    }
}