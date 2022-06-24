﻿using System;

namespace Entity
{
    public class Estacionamiento
    {

        public int NumeroEspacio { get; set; }
        public int NumeroPiso { get; set; }
        public Boolean Estado { get; set; }

        public Estacionamiento(int numeroEspacio)
        {
            NumeroEspacio = numeroEspacio;
            NumeroPiso = 0;
            Estado = false;
        }

        public Estacionamiento(int numeroEspacio, int numeroPiso)
        {
            NumeroEspacio = numeroEspacio;
            NumeroPiso = numeroPiso;
            Estado = false;
        }

        public Estacionamiento(int numeroEspacio, int numeroPiso, Boolean estado)
        {
            NumeroEspacio = numeroEspacio;
            NumeroPiso = numeroPiso;
            Estado = estado;
        }
        public Estacionamiento()
        {
        }
    }
}