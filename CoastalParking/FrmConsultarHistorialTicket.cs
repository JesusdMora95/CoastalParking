using Entity;
using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;

namespace CoastalParking
{
    public partial class FrmConsultarHistorialTicket : Form
    {
        TiquetParticularService service;
        TarifaService tarifaService;
        public FrmConsultarHistorialTicket()
        {
            service = new TiquetParticularService(ConfigConnectionString.ConnectionString);
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private void labeLConsultar_Click(object sender, EventArgs e)
        {
            TiquetParticular tiquet = new TiquetParticular();
            tiquet = service.ConsultarPorPlaca(txtPlaca.Text);
            LlenarGrid(tiquet);
        }

        private void ConfiguraionInicalGrid()
        {
            Tabla.AllowUserToAddRows = false;
            Tabla.Columns.Add("Codigo", "HoraEntrada");
            Tabla.Columns.Add("HoraSalida", "Estado");
            Tabla.Columns.Add("ValorExtra", "ValorTotal");
            Tabla.Columns.Add("ValorMinimo", "Tipo");
            Tabla.Columns.Add("NumeroEspacio","");
            Tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            Tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Tabla.ColumnHeadersDefaultCellStyle.Font =
                new Font(Tabla.Font, FontStyle.Bold);
        }

        public void LlenarGrid(TiquetParticular item)
        {
            Tabla.Rows.Clear();
            foreach (Tarifa tarifa in tarifaService.Consultar().Tarifas)
            {
                if (tarifa.TipoVehiculo == item.Tipo1)
                {
                    item.Tipo = Convert.ToString(tarifa.NombreTipodeVehiculo);
                }
            }
            Tabla.Rows.Add(item.Codigo, item.HoraEntrada, item.HoraSalida, item.EstadoTiquet, item.ValorExtra, item.ValorTotal, item.ValorMinimo, item.Tipo, item.NumeroEspacio1);
            Tabla.Refresh();
        }
    }
}