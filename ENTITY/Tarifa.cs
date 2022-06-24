using System;
using System.Net.Mail;

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


        public Tarifa(int tipoVehiculo, string nombreTipodeVehiculo, DateTime fecha, double valorMinimo, double valorNormal, int tiempoMinimo)
        {
            TipoVehiculo = tipoVehiculo;
            NombreTipodeVehiculo = nombreTipodeVehiculo;
            Fecha = fecha;
            ValorMinimo = valorMinimo;
            ValorNormal = valorNormal;
            TiempoMinimo = tiempoMinimo;
        }

        public Tarifa()
        {
        }
    }
}