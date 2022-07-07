using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoastalParking
{
    public class Validacion
    {
        public bool ValidarCampoVacio(Control objeto, ErrorProvider errorProvider)
        {
            bool error = false;
            foreach (var item in objeto.Controls)
            {
                if (item is TextBox)
                {
                    TextBox obj = (TextBox)item;

                    if (obj.Text.Equals(""))
                    {
                        errorProvider.SetError(obj, "No Puede Estar Vacio");
                        error = true;
                    }
                    else
                    {
                        errorProvider.SetError(obj, "");
                        error = false;
                    }
                }
            }
            return error;
        }

        public bool ValidarCampoVacioEspecifico(TextBox obj, ErrorProvider errorProvider)
        {
            bool error = false;
            if (obj.Text.Equals(""))
            {
                errorProvider.SetError(obj, "No Puede Estar Vacio");
                error = true;
            }
            else
            {
                errorProvider.SetError(obj, "");
                error = false;
            }
            return error;
        }

        public void LimpiarCajasDeTextoYComboBox(Control control)
        {
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                else if (txt is ComboBox)
                {
                    ((ComboBox)txt).Text = "";
                }
            }
        }
    }
}
