using System;
using System.Collections.Generic;
using DAL;
using Entity;
using Infraesctructura;

namespace BLL
{
    public class TarifaService
    {

        TarifaRepository tarifaRepository;
        ConnectionManager connectionManager;
        public TarifaService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            tarifaRepository = new TarifaRepository(connectionManager.Connection);
        }
        public string Guardar(Tarifa tarifa)
        {
            try
            {
                connectionManager.Open();
                if (tarifaRepository.BuscarPorTipoVehiculo(Convert.ToString(tarifa.TipoVehiculo)) == null)
                {
                    tarifaRepository.Guardar(tarifa);
                    return "Datos Guardados Satisfactoriamente";

                }
                return $"La Tarifa con el tipo de vehiculo {tarifa.NombreTipodeVehiculo} ya se encuentra registrada";

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

        public Tarifa ConsultarTarifa(string tipo)
        {
            try
            {
                connectionManager.Open();
                return tarifaRepository.BuscarPorTipoVehiculo(tipo);
            }
            finally
            {
                connectionManager.Close();
            }
        }

        public int TotalElemtos()
        {
            try
            {
                connectionManager.Open();
                return tarifaRepository.TotalElementos();
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
                return new ConsultaResponse(tarifaRepository.Consultar());
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

        public ConsultaResponse ConsultarPorNombreVehiculo(string nombre)
        {
            try
            {
                connectionManager.Open();
                return new ConsultaResponse(tarifaRepository.FiltrarPorNombre(nombre));
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


        /* public BusquedaReponse ConsultaUltimoTiquet(string text)
         {
             BusquedaReponse busquedaReponse;
             try
             {
                 connectionManager.Open();
                 busquedaReponse = new BusquedaReponse(tarifaRepository.BuscarCodigo(text));
                 if (busquedaReponse.Error)
                 {

                     return busquedaReponse = new BusquedaReponse("No");
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
         }*/

        public class ConsultaResponse
        {
            public List<Tarifa> Tarifas { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Tarifa> tarifa)
            {
                Tarifas = tarifa;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }

        }


        public string ExistenciaDeVehiculo(string nombre)
        {
            try
            {
                connectionManager.Open();
                return tarifaRepository.ExistenciaVehiculo(nombre);
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

        public class BusquedaReponse
        {
            public Tarifa Tarifa { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public BusquedaReponse(Tarifa tarifa)
            {
                Tarifa = tarifa;
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