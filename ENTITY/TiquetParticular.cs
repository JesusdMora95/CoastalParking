using System;

namespace Entity
{
    public class TiquetParticular : Tiquet
    {
        public double ValorMinimoTarifa { get; set; }
        public double ValorNormalTarifa { get; set; }
        public int TiempoMinimo { get; set; }
        public double ValorMinimo { get; set; }

        public TiquetParticular(string codigo, DateTime horaEntrada, int numeroEstacionamiento, int numeroPiso, string placa, string tipo, double valorMinimoTarifa, double valorNormalTarifa, int tiempoMinimo)
        {
            Codigo = codigo;
            HoraEntrada = horaEntrada;
            HoraSalida = DateTime.Now;
            EstadoTiquet = false;
            NumeroEspacio = numeroEstacionamiento;
            NumeroPiso = numeroPiso;
            Placa = placa;
            Tipo = tipo;
            ValorMinimoTarifa = valorMinimoTarifa;
            ValorNormalTarifa = valorNormalTarifa;
            TiempoMinimo = tiempoMinimo;
            CalcularValorTiquet();
        }

        public TiquetParticular(string codigo, DateTime horaEntrada, DateTime horaSalida, bool estadoTiquet, double valorExtra, double valorTotal, int numeroEspacio, int numeroPiso, string placa, string tipo, double valorMinimoTarifa, double valorNormalTarifa, int tiempoMinimo, double valorMinimo)
        {
            Codigo = codigo;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
            EstadoTiquet = estadoTiquet;
            ValorExtra = valorExtra;
            ValorTotal = valorTotal;
            NumeroEspacio = numeroEspacio;
            NumeroPiso = numeroPiso;
            Placa = placa;
            Tipo = tipo;
            ValorMinimoTarifa = valorMinimoTarifa;
            ValorNormalTarifa = valorNormalTarifa;
            TiempoMinimo = tiempoMinimo;
            ValorMinimo = valorMinimo;
        }

        public TiquetParticular()
        {
        }

        public override void CalcularValorTiquet()
        {
            if (CantidadDeHoras() <= 0)
            {
                ConMinutos();
            }
            else
            {
                ConHoras();
            }

        }

        private void ConMinutos()
        {
            ValorMinimo = CantidadDeMinutosEnHoras() * ValorMinimoTarifa;
            ValorTotal = ValorMinimo + ValorExtra;
        }

        private void ConHoras()
        {
            if (CantidadDeHoras() <= TiempoMinimo)
            {
                ValorMinimo = CantidadDeHoras() * ValorMinimoTarifa;
            }
            else
            {
                ValorMinimo = TiempoMinimo * ValorMinimoTarifa;
            }
            if (CantidadDeHoras() > TiempoMinimo)
            {
                ValorExtra = CantidadDeHoras() - TiempoMinimo * ValorNormalTarifa;
            }
            ValorTotal = ValorMinimo + ValorExtra;
        }
    }
}