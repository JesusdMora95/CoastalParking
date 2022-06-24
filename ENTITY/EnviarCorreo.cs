using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Entity;

namespace ENTITY
{
    public class EnviarCorreo
    {
        MailMessage mail;
        SmtpClient smtp;


        public void ConfigurarSmtp()
        {
            smtp.Host = "smtp.gamil.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("jennifer.cuello2008@gmail.com", "Pandapolar3045381013");


        }


        public void ConfigurarMail(Tarifa tarifa)
        {
            mail = new MailMessage();
            mail.From = new MailAddress("MiAplicacion@unicesar.com");
            mail.To.Add(tarifa.Mail);
            mail.Subject = $"registro de usuario {tarifa.NombreTipodeVehiculo}";
            mail.Body = $"<b>sr {tarifa.NombreTipodeVehiculo} </b> <br> se ha registrado correctamente";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
        }

        public string EnviarGmail(Tarifa tarifa)
        {
            try
            {
                ConfigurarSmtp();
                ConfigurarMail(tarifa);
                smtp.Send(mail);
                return "correo enviado";
            }
            catch (Exception e)
            {

                return "error en enviar correo " + e.Message;
            }
            finally
            {
                mail.Dispose();
            }
        }


    }
}