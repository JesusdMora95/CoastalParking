using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoastalParking
{
    public partial class FrmRegistrarLogin : Form
    {
        LoginService loginService;
        Validacion Validacion = new Validacion();
        FrmLogin Frm = new FrmLogin();
        public FrmRegistrarLogin()
        {
            loginService = new LoginService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (Validacion.ValidarCampoVacio(this,errorProvider1) == false)
            {
                MessageBox.Show(loginService.Guardar(CrearLogin()));
            }
        }

        private Login CrearLogin()
        {
            Login login = new Login();
            login.Usuario = txtUsuario.Text;
            login.Contraseña = txtContraseña.Text;
            return login;
        }

        private void btModificar_Click(object sender, EventArgs e)
        {
            if (FrmLogin.dato.Equals("admin"))
            {
                if (Validacion.ValidarCampoVacio(this, errorProvider1) == false && Validacion.ValidarCampoVacio(this, errorProvider1) == false)
                {
                    MessageBox.Show(loginService.Modificar(CrearLogin()));
                }
                else
                {
                    errorProvider1.SetError(txtUsuario, "No Puede Estar Vacio");
                }
            }
            else
            {
                MessageBox.Show("No Tiene Acceso Para Modificar Este Formulario");
            }
        }

        private void btBorrar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
        }
    }
}
