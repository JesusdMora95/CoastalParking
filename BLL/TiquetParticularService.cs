using System;
using System.Collections.Generic;
using Entity;
using DAL;
using Infraesctructura;

namespace BLL
{
    public class TiquetParticularService
    {

        TiquetParticularRepository tiquetParticularRepository;
        ConnectionManager connectionManager;
        public TiquetParticularService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            tiquetParticularRepository = new TiquetParticularRepository(connectionManager.Connection);
        }

        public string Guardar(TiquetParticular tiquetParticular)
        {
            try
            {
                connectionManager.Open();
                if (tiquetParticularRepository.BuscarPorCodigo(tiquetParticular.Codigo) == null)
                {
                    tiquetParticularRepository.Guardar(tiquetParticular);
                    return "Datos Guardados Satisfactoriamente";
                }
                return $"El vehiculo con la placa {tiquetParticular.Codigo} ya se encuentra registrado";

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
                return new ConsultaResponse(tiquetParticularRepository.Consultar());
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
            public List<TiquetParticular> tiquetParticulars { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<TiquetParticular> tiquetParticular)
            {
                tiquetParticulars = tiquetParticular;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }

        }

        public BusquedaReponse BuscarCodigo()
        {
            BusquedaReponse busquedaReponse;
            try
            {
                connectionManager.Open();
                busquedaReponse = new BusquedaReponse(tiquetParticularRepository.BuscarCodigo());
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

        public BusquedaReponse Buscar(string placa)
        {
            BusquedaReponse busquedaReponse;
            try
            {
                connectionManager.Open();
                busquedaReponse = new BusquedaReponse(tiquetParticularRepository.BuscarPorCodigo(placa));
                if (busquedaReponse.TiquetParticular == null)
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
            public TiquetParticular TiquetParticular { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public BusquedaReponse(TiquetParticular tiquetParticular)
            {
                TiquetParticular = tiquetParticular;
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