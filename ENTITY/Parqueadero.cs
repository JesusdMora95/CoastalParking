using System.Collections.Generic;

namespace Entity
{
    public class Parqueadero
    {
        public int EspacioTotal { get; set; }
        public int EspacioDisponible { get; set; }
        public List<Estacionamiento> Estacionamientos { get; set; }

        public Parqueadero(int espacioTotal, List<Estacionamiento> estacionamiento)
        {
            EspacioTotal = espacioTotal;
            Estacionamientos = estacionamiento;
            ActualizarEspacios();
        }

        public Parqueadero(int espacioTotal, int espacioDisponible, List<Estacionamiento> estacionamientos)
        {
            EspacioTotal = espacioTotal;
            EspacioDisponible = espacioDisponible;
            Estacionamientos = estacionamientos;
        }

        public Parqueadero()
        {
        }

        public void ActualizarEspacios()
        {
            int numeroOcupados = 0, numeroDesocupados = 0;
            foreach (var item in Estacionamientos)
            {
                if (item.Estado == "false")
                {
                    numeroDesocupados = numeroDesocupados + 1;
                }
                else
                {
                    numeroOcupados = numeroOcupados + 1;
                }
            }
            if (EspacioTotal == numeroOcupados + numeroDesocupados)
            {
                EspacioDisponible = numeroDesocupados;
            }
        }
    }
}