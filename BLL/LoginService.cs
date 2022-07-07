using DAL;
using ENTITY;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class LoginService
    {
        LoginRepository loginRepository;
        ConnectionManager connectionManager;
        public LoginService(string connectionstring)
        {
            connectionManager = new ConnectionManager(connectionstring);
            loginRepository = new LoginRepository(connectionManager.Connection);
        }

        public string Guardar(Login login)
        {
            try
            {
                connectionManager.Open();
                if (loginRepository.VerificarLogin(login.Usuario) == null)
                {
                    loginRepository.Guardar(login);
                    return $"Usuario Guardado Satisfactoriamente";
                }
                else
                {
                    return $"El Usuario Ya Existe";
                }
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
                return new ConsultaResponse(loginRepository.Consultar());
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
            public List<Login> logins { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Login> Logins)
            {
                logins = Logins;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }
        }

        public string Modificar(Login login)
        {
            try
            {
                connectionManager.Open();
                loginRepository.Modificar(login);
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