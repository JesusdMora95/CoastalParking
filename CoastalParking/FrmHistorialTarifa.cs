using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Entity;
using BLL;
using static BLL.TarifaService;

namespace CoastalParking
{
    public partial class FrmHistorialTarifa : Form
    {
        public Tarifa tarifa;
        public TarifaService tarifaService;
        public FrmHistorialTarifa()
        {
            InitializeComponent();
            tarifa = new Tarifa();
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
            ConfiguraionInicalGrid();
            ValidarRespuestadeConsulta(tarifaService.Consultar());
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private ConsultaResponse SeleccionDeConsulta()
        {
            string filtro = txtNombre.Text;
            return tarifaService.ConsultarPorPalabraTipoVehiculo(filtro);
        }


        private void ValidarRespuestadeConsulta(ConsultaResponse respuetsa)
        {
            if (respuetsa.Error)
            {
                MessageBox.Show(respuetsa.Mensaje, "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LlenarGrid(respuetsa.Tarifas);
            }
        }

        public void LlenarGrid(List<Tarifa> tarifas)
        {
            Tabla.Rows.Clear();
            foreach (var item in tarifas)
            {
                Tabla.Rows.Add(item.TipoVehiculo, item.NombreTipodeVehiculo, item.Fecha, item.ValorMinimo, item.ValorNormal, item.TiempoMinimo);
            }
            Tabla.Refresh();
        }

        private void ConfiguraionInicalGrid()
        {
            Tabla.AllowUserToAddRows = false;
            Tabla.Columns.Add("TipoVehiculo", "Tipo Vehiculo");
            Tabla.Columns.Add("NombreTipodeVehiculo", "Nombre");
            Tabla.Columns.Add("Fecha", "Fecha");
            Tabla.Columns.Add("ValorMinimo", "Valor minimo");
            Tabla.Columns.Add("ValorNormal", "Valor normal");
            Tabla.Columns.Add("TiempoMinimo", "Tiempo minimo");
            Tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            Tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Tabla.ColumnHeadersDefaultCellStyle.Font =
                new Font(Tabla.Font, FontStyle.Bold);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            SeleccionDeConsulta();
        }
    }
}