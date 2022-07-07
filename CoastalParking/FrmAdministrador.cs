using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CoastalParking
{
    public partial class FrmAdministrador : Form
    {
        public FrmAdministrador()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public void AbrirFormEnPanel(object formularioFactura)
        {
            if (this.PanelContenedor.Controls.Count > 0)
                this.PanelContenedor.Controls.RemoveAt(0);
            Form fF = formularioFactura as Form;
            fF.TopLevel = false;
            fF.Dock = DockStyle.Fill;
            this.PanelContenedor.Controls.Add(fF);
            this.PanelContenedor.Tag = fF;
            fF.Show();
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            Restaurar.Visible = false;
            Maximizar.Visible = true;
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            if (MessageBox.Show("Esta seguro de cerrar sesion?", "Warning",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Dispose();
                login.ShowDialog();
            }
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Maximizar.Visible = false;
            Restaurar.Visible = true;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmRegistrarEspacio());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmRegistrarTarifa());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmConsultarHistorial());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmRegistrarTicketAlquiler());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmRealizarPagoAlquiler());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmConsultarHistorialTicket());
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FrmRegistrarLogin());
        }
    }
}
