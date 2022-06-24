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
            // EnviarCorreo enviar = new EnviarCorreo();
            // string mensajeEmail = string.Empty;
            try
            {
                connectionManager.Open();
                if (tarifaRepository.BuscarPorTipoVehiculo(Convert.ToString(tarifa.TipoVehiculo)) == null)
                {
                    tarifaRepository.Guardar(tarifa);
                    //  mensajeEmail = enviar.EnviarGmail(tarifa);
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

        public BusquedaReponse ConsultaUltimoTiquet(string text)
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
        }

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

        public string Modificar(Tarifa tarifaNueva, string tipodeVehicula)
        {

            try
            {
                connectionManager.Open();

                if (tarifaRepository.BuscarPorTipoVehiculo(tipodeVehicula) != null)
                {
                    tarifaRepository.Modificar(tarifaNueva, tipodeVehicula);
                    return $"Se Modificó la tarifa con tipo de vehiculo {tipodeVehicula}";
                }
                return $"No se encontró la tarifa con tipo de vehiculo{tipodeVehicula}";
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


        public ConsultaResponse ConsultarPorTipoVehiculo(string tipo)
        {
            try
            {
                connectionManager.Open();
                return new ConsultaResponse(tarifaRepository.FiltrarPorTipoVehiculo(tipo));
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

        public ConsultaResponse ConsultarPorPalabraTipoVehiculo(string tipo)
        {
            try
            {
                connectionManager.Open();
                return new ConsultaResponse(tarifaRepository.FiltrarPorTipoVehiculo(tipo));
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

        public string GenerarPdf(Tarifa tarifa, string filename)
        {
            DocumentoPdf documentoPdf = new DocumentoPdf();
            string mensajeEmail = string.Empty;
            try
            {
                documentoPdf.GuardarPdf(tarifa, filename);
                return "Se generó el Documento satisfactoriamente:" + mensajeEmail;
            }
            catch (Exception e)
            {
                return "Error al crear docuemnto" + e.Message;
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