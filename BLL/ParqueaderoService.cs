using System;
using System.Collections.Generic;
using DAL;
using Entity;

namespace BLL
{
    public class ParqueaderoService
    {
        ParqueaderoRepository parqueaderoRepository;
        ConnectionManager connectionManager;
        public ParqueaderoService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            parqueaderoRepository = new ParqueaderoRepository(connectionManager.Connection);
        }
        public string Guardar(int EspacioTotal, int EspacioDisponible)
        {
            try
            {
                connectionManager.Open();
                parqueaderoRepository.Guardar(EspacioTotal,EspacioDisponible);
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
                return new ConsultaResponse(parqueaderoRepository.Consultar());
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
            public List<Parqueadero> parqueaderos { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Parqueadero> parqueadero)
            {
                parqueaderos = parqueadero;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }
        }

        public string Modificar(int EspacioTotal, int EspacioDisponible,int total)
        {
            try
            {
                connectionManager.Open();
                parqueaderoRepository.Modificar(EspacioTotal,EspacioDisponible,total);
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

        public int ConsultarTotal()
        {
            try
            {
                connectionManager.Open();
                return parqueaderoRepository.ObtenerTotalRegistros();
            }catch(Exception e)
            {
                return 0;
            }
            finally
            {
                connectionManager.Close();
            }
        }
    }
}