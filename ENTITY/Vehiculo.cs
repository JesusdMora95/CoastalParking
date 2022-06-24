using System;

namespace Entity
{
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string ModeloyMarca { get; set; }
        public string AplicaConvenio { get; set; }

        public Vehiculo(string placa, string tipo, string modeloyMarca, string aplicaconvenio)
        {
            Placa = placa;
            Tipo = tipo;
            ModeloyMarca = modeloyMarca;
            AplicaConvenio = aplicaconvenio;
        }

        public Vehiculo()
        {
        }
    }
}