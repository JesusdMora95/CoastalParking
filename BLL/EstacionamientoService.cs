using DAL;
using Entity;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EstacionamientoService
    {
        EstacionamientoRepository estacionamientoRepository;
        ConnectionManager connectionManager;
        public EstacionamientoService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            estacionamientoRepository = new EstacionamientoRepository(connectionManager.Connection);
        }
        public string Guardar(Estacionamiento estacionamiento)
        {
            try
            {
                connectionManager.Open();
                estacionamientoRepository.Guardar(estacionamiento);
                return $"Registro Satisfactorio";
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
                return new ConsultaResponse(estacionamientoRepository.Consultar());
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
            public List<Estacionamiento> estacionamientos { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Estacionamiento> estacionamiento)
            {
                estacionamientos = estacionamiento;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }
        }

        public string Modificar(Estacionamiento estacionamiento)
        {
            try
            {
                connectionManager.Open();
                estacionamientoRepository.Modificar(estacionamiento);
                return $"Se Modificó correctamente";
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
    }
}