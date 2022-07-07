using System;

namespace Entity
{
    public abstract class Tiquet
    {
        public string Codigo { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public Boolean EstadoTiquet { get; set; }
        public double ValorExtra { get; set; }
        public double ValorTotal { get; set; }
        public int NumeroEspacio { get; set; }
        public int NumeroPiso { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }

        public abstract void CalcularValorTiquet();

        public int CantidadDeHoras()
        {
            TimeSpan resultado = HoraSalida - HoraEntrada;
            return resultado.Hours;
        }

        public double CantidadDeMinutosEnHoras()
        {
            TimeSpan resultado = HoraSalida - HoraEntrada;
            return resultado.Minutes / 60;
        }

    }
}