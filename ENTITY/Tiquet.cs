using System;

namespace Entity
{
    public abstract class Tiquet
    {
        public string Codigo { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public string EstadoTiquet { get; set; }
        public decimal ValorExtra { get; set; }
        public decimal ValorTotal { get; set; }
        public int NumeroEspacio { get; set; }
        public int NumeroPiso { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }

        public abstract void CalcularValorTiquet();

        public int CantidadDeHoras()
        {
            TimeSpan resultado = Convert.ToDateTime(HoraSalida) - Convert.ToDateTime(HoraEntrada);
            return resultado.Hours;
        }

        public decimal CantidadDeMinutosEnHoras()
        {
            TimeSpan resultado = Convert.ToDateTime(HoraSalida) - Convert.ToDateTime(HoraEntrada);
            return resultado.Minutes / 60;
        }

    }
}