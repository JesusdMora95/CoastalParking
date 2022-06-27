using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CoastalParking
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Equals("admin") && txtPass.Text.Equals("123"))
            {
                FrmAdministrador frmAdministrador = new FrmAdministrador();
                frmAdministrador.Visible = true;
                this.Visible = false;
            }
            else 
            {
                MessageBox.Show("Usuario o contraseña no valido");
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
