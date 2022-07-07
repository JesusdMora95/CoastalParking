using System;

namespace Entity
{
    public class TiquetParticular : Tiquet
    {
        public decimal ValorMinimoTarifa { get; set; }
        public decimal ValorNormalTarifa { get; set; }
        public int TiempoMinimo { get; set; }
        public decimal ValorMinimo { get; set; }

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