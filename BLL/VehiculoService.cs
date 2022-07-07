using System;
using System.Collections.Generic;
using DAL;
using Entity;

namespace BLL
{
    public class VehiculoService
    {
        VehiculoRepository vehiculoRepository;
        ConnectionManager connectionManager;
        public VehiculoService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            vehiculoRepository = new VehiculoRepository(connectionManager.Connection);
        }

        public string Guardar(Vehiculo vehiculo)
        {
            try
            {
                connectionManager.Open();
                if (vehiculoRepository.BuscarPorPlaca(vehiculo.Placa) == null)
                {
                    vehiculoRepository.Guardar(vehiculo);
                    return "Datos Guardados Satisfactoriamente";
                }
                return $"El vehiculo con la placa {vehiculo.Placa} ya se encuentra registrado";
            }
            catch (Exception exception)
            {
                return "Se presentó el siguiente error:" + exception.Message;
            }
            finally
            {
                connectionManager.Close();
            }
        }

        public string BuscaporPlaca(string placa)
        {
            try
            {
                connectionManager.Open();
                var vehiculo = (vehiculoRepository.BuscarPorPlaca(placa));
                if (vehiculo != null)
                {
                    return vehiculo.AplicaConvenio;
                }
                return "Sin datos";
            }
            catch (Exception exception)
            {
                return "Se presentó el siguiente error:" + exception.Message;
            }
            finally
            {
                connectionManager.Close();
            }
        }

        public ConsultaResponse Consultar()
        {
            try
            {
                connectionManager.Open();
                return new ConsultaResponse(vehiculoRepository.Consultar());
            }
            catch (Exception exception)
            {
                return new ConsultaResponse("Se presentó el siguiente error:" + exception.Message);
            }
            finally
            {
                connectionManager.Close();
            }
        }

        public class ConsultaResponse
        {
            public List<Vehiculo> Vehiculos { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Vehiculo> vehiculos)
            {
                Vehiculos = vehiculos;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }
        }

        public void Modificar(Vehiculo vehiculo)
        {
            connectionManager.Open();
            vehiculoRepository.Modificar(vehiculo);
            connectionManager.Close();
        }

        public BusquedaReponse Buscar(string placa)
        {
            BusquedaReponse busquedaReponse;
            try
            {
                connectionManager.Open();
                busquedaReponse = new BusquedaReponse(vehiculoRepository.BuscarPorPlaca(placa));
                if (busquedaReponse.Vehiculo == null)
                {
                    return busquedaReponse = new BusquedaReponse("No se encontraron resultados");
                }
                else
                {
                    return busquedaReponse;
                }
            }
            catch (Exception exception)
            {
                return busquedaReponse = new BusquedaReponse("Se presentó el siguiente error:" + exception.Message);
            }
            finally
            {
                connectionManager.Close();
            }
        }

        public class BusquedaReponse
        {
            public Vehiculo Vehiculo { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public BusquedaReponse(Vehiculo vehiculo)
            {
                Vehiculo = vehiculo;
                Error = false;
            }

            public BusquedaReponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }
        }
    }
}