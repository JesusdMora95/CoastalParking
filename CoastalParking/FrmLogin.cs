using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BLL;

namespace CoastalParking
{
    public partial class FrmLogin : Form
    {
        public string dato = "admin";
        public string dato1 = "verga";
        LoginService loginService;
        Validacion validacion = new Validacion();
        public FrmLogin()
        {
            loginService = new LoginService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validacion.ValidarCampoVacioEspecifico(txtUser,errorProviderLogin) == false && validacion.ValidarCampoVacioEspecifico(txtPass, errorProviderLogin) == false) {
                foreach (var login in loginService.Consultar().logins)
                {
                    if (txtUser.Text == login.Usuario && txtPass.Text == login.Contraseña)
                    {
                        FrmAdministrador frmAdministrador = new FrmAdministrador();
                        frmAdministrador.Visible = true;
                        this.Visible = false;
                    }
                    else if (txtUser.Text != login.Usuario)
                    {
                        errorProviderLogin.SetError(txtUser, "Usuario Incorrecto");
                    }else if (txtPass.Text != login.Contraseña)
                    {
                        errorProviderLogin.SetError(txtPass, "Contraseña Incorrecto");
                    }
                }
            }
            limpiar();
        }

        private void limpiar()
        {
            txtUser.Text = "";
            txtPass.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de cerrar la aplicacion?", "Warning",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void BarraFrontal_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
