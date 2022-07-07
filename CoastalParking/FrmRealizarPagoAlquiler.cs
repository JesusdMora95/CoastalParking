using System;
using System.Windows.Forms;
using BLL;
using Entity;
using ENTITY;

namespace CoastalParking
{
    public partial class FrmRealizarPagoAlquiler : Form
    {
        TiquetParticularService tiquetParticularService;
        TarifaService tarifaService;
        public FrmRealizarPagoAlquiler()
        {
            tiquetParticularService = new TiquetParticularService(ConfigConnectionString.ConnectionString);
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private void labelBuscar_Click(object sender, EventArgs e)
        {
            TiquetParticular tiquetParticular = new TiquetParticular();
            tiquetParticular = tiquetParticularService.ConsultarPorPlaca(txtConsultar.Text);
            txtPlaca.Text = tiquetParticular.Placa;
            txtHoraEntrada.Text = tiquetParticular.HoraEntrada;
            tiquetParticular.HoraSalida = Convert.ToString(DateTime.Now);
            txtHoraSalida.Text = tiquetParticular.HoraSalida;
            Tarifa tarifa = new Tarifa();
            tarifa = tarifaService.ConsultarTarifa(Convert.ToString(tiquetParticular.Tipo1));
            MessageBox.Show(Convert.ToString(Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute)));
            MessageBox.Show(Convert.ToString(Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute)));
            MessageBox.Show(Convert.ToString(Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute - Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute)));
            if (Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute - Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute) <= tarifa.TiempoMinimo)
            {
                txtValorMinimoTiempo.Text = Convert.ToString(Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute - Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute) * tarifa.ValorMinimo);
                txTotal.Text = txtValorMinimoTiempo.Text;
                txtValorTarifaNormal.Text = Convert.ToString(tarifa.ValorMinimo);
            }
            else if (Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute - Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute) > tarifa.TiempoMinimo)
            {
                txtValorMinimoTiempo.Text = Convert.ToString(tarifa.TiempoMinimo * tarifa.ValorMinimo);
                txtValorExtraTiempo.Text = Convert.ToString(tarifa.ValorNormal);
                txtValorTarifaNormal.Text = Convert.ToString(tarifa.ValorMinimo);
                txtTarifaExtra.Text = Convert.ToString(Convert.ToDouble(Convert.ToDateTime(tiquetParticular.HoraSalida).Minute - Convert.ToDateTime(tiquetParticular.HoraEntrada).Minute) - tarifa.TiempoMinimo * tarifa.ValorNormal);
                txTotal.Text = Convert.ToString(Convert.ToInt32(txtValorMinimoTiempo.Text + txtTarifaExtra.Text));
            }
        }
    }
}