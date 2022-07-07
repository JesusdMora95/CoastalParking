using System;
using System.Windows.Forms;
using Entity;
using BLL;

namespace CoastalParking
{
    public partial class FrmRegistrarTarifa : Form
    {
        TarifaService tarifaService;
        Validacion validacion = new Validacion();
        public FrmRegistrarTarifa()
        {
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private Tarifa CrearTarifa()
        {
            Tarifa tarifa = new Tarifa();
            tarifa.TipoVehiculo = tarifaService.TotalElemtos() + 1;
            tarifa.NombreTipodeVehiculo = txtNombreVehiculo.Text;
            tarifa.Fecha = DateTime.Now;
            tarifa.ValorMinimo = Convert.ToDouble(txtValorTarifaMinima.Text);
            tarifa.ValorNormal = Convert.ToDouble(txtValorTarifaExtra.Text);
            tarifa.TiempoMinimo = Convert.ToInt32(txtTiempoMinimo.Text);
            return tarifa;
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            if(validacion.ValidarCampoVacio(this,errorProvider1) == false)
            {
                MessageBox.Show(tarifaService.Guardar(CrearTarifa()));
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            FrmHistorialTarifa frmHistorial = new FrmHistorialTarifa();
            frmHistorial.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtNombreVehiculo.Text = "";
            txtValorTarifaMinima.Text = "";
            txtValorTarifaExtra.Text = "";
            txtTiempoMinimo.Text = "";
        }

        private void labelConsultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tarifaService.ExistenciaDeVehiculo(txtConsulta.Text));
        }
    }
}