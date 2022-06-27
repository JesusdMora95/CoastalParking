using System;
using System.Windows.Forms;
using Entity;
using BLL;

namespace CoastalParking
{
    public partial class FrmRegistrarTarifa : Form
    {
        public TarifaService tarifaService;
        public Tarifa tarifa;
        public FrmRegistrarTarifa()
        {
            InitializeComponent();
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
            tarifa = new Tarifa();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tarifa = MapearTarifa();
            string mensaje = tarifaService.Guardar(tarifa);
            GuardarPdf(tarifa);
            MessageBox.Show(mensaje, "Guardar Tarifa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void GuardarPdf(Tarifa tarifa)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Guardar Informe";
            saveFileDialog.InitialDirectory = @"C:/";
            saveFileDialog.DefaultExt = "pdf";
            string filename = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog.FileName;

                if (filename != "")
                {
                    string mensaje = tarifaService.GenerarPdf(tarifa, filename);
                    MessageBox.Show(mensaje, "Generar Pdf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se especifico una ruta o No hay datos para generar el reporte", "Generar Pdf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private Tarifa MapearTarifa()
        {
            Random r = new Random();
            tarifa.TipoVehiculo = r.Next(1, 101);
            tarifa.NombreTipodeVehiculo = txtTipoVehiculo.Text.ToUpper();
            tarifa.Fecha = DateTime.Now;
            tarifa.ValorMinimo = Convert.ToDouble(txtTarifaMinima.Text);
            tarifa.ValorNormal = Convert.ToDouble(txtValorTarifa.Text);
            tarifa.TiempoMinimo = Convert.ToInt32(txtTiempoMinimo.Text);
            return tarifa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTipoVehiculo.Text = "";
            txtTarifaMinima.Text = "";
            txtValorTarifa.Text = "";
            txtTiempoMinimo.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmHistorialTarifa historialTarifa = new FrmHistorialTarifa();
            historialTarifa.Show();
        }
    }
}