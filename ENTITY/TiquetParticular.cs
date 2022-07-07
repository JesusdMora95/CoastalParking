using System;

namespace Entity
{
    public class TiquetParticular : Tiquet
    {
        public double ValorMinimoTarifa { get; set; }
        public double ValorNormalTarifa { get; set; }
        public int TiempoMinimo { get; set; }
        public double ValorMinimo { get; set; }

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