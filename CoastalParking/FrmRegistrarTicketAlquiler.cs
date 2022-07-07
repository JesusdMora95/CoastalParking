using System;
using System.Windows.Forms;
using Entity;
using BLL;

namespace CoastalParking
{
    public partial class FrmRegistrarTicketAlquiler : Form
    {
        static public VehiculoService vehiculoService;
        static public TiquetParticularService tiquetParticularService;
        EstacionamientoService estacionamientoService;
        TarifaService tarifa;
        Validacion validacion = new Validacion();
        public FrmRegistrarTicketAlquiler()
        {
            tarifa = new TarifaService(ConfigConnectionString.ConnectionString);
            estacionamientoService = new EstacionamientoService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
            vehiculoService = new VehiculoService(ConfigConnectionString.ConnectionString);
            tiquetParticularService = new TiquetParticularService(ConfigConnectionString.ConnectionString);
            LlenarTipoVehiculo();
            LlenarNumeroEspacios();
        }

        private void brGuardar_Click(object sender, EventArgs e)
        {
            if(validacion.ValidarCampoVacio(this,errorProvider1) == false)
            {
                MessageBox.Show(tiquetParticularService.Guardar(CrearTiket()));
            }
        }

        private TiquetParticular CrearTiket()
        {
            TiquetParticular tiquet = new TiquetParticular();
            tiquet.Codigo = Convert.ToString(tiquetParticularService.TotalElemtos());
            tiquet.HoraEntrada = Convert.ToString(DateTime.Now);
            tiquet.HoraSalida = Convert.ToString(DateTime.Now);
            tiquet.EstadoTiquet = "false";
            tiquet.ValorExtra = 0;
            tiquet.ValorTotal = 0;
            tiquet.ValorMinimo = 0;
            //tiquet.Tipo = cmbTipoVehiculo.Text;
            tiquet.Placa = txtPlaca.Text;
            foreach (Tarifa item in tarifa.ConsultarPorNombreVehiculo(cmbTipoVehiculo.Text).Tarifas)
            {
                tiquet.Tipo = Convert.ToString(item.TipoVehiculo);
            }
            tiquet.NumeroEspacio = Convert.ToInt32(comboNumeroEspacio.Text);
            return tiquet;
        }

        private void btModificar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tiquetParticularService.Modificar(ModificarTiquet(CrearTiket())));
        }

        private TiquetParticular ModificarTiquet(TiquetParticular tiquet)
        {
            tiquet.Codigo = Convert.ToString(valor);
            foreach(Tarifa item in tarifa.ConsultarPorNombreVehiculo(cmbTipoVehiculo.Text).Tarifas)
            {
                tiquet.NumeroEspacio = item.TipoVehiculo;
            }
            return tiquet;
        }

        private void LlenarTipoVehiculo()
        {
            foreach(Tarifa tarifa in tarifa.Consultar().Tarifas)
            {
                if (cmbTipoVehiculo.Items.Contains(tarifa.NombreTipodeVehiculo) == false)
                {
                    cmbTipoVehiculo.Items.Add(tarifa.NombreTipodeVehiculo);
                }
            }
        }

        private void LlenarNumeroEspacios()
        {
            foreach (Estacionamiento item in estacionamientoService.Consultar().estacionamientos)
            {
                comboNumeroEspacio.Items.Add(item.NumeroEspacio);
            }
        }

        int valor = 0;
        private void label8_Click(object sender, EventArgs e)
        {
            TiquetParticular tiquet = new TiquetParticular();
            tiquet = tiquetParticularService.ConsultarPorPlaca(txtPlaca.Text);
            valor = Convert.ToInt32(tiquet.Codigo);
            txtPlaca.Text = tiquet.Placa;
            foreach (Tarifa item in tarifa.Consultar().Tarifas)
            {
                if (item.TipoVehiculo == tiquet.Tipo1)
                {
                    cmbTipoVehiculo.Text = Convert.ToString(item.NombreTipodeVehiculo);
                }
            }
            comboNumeroEspacio.Text = Convert.ToString(tiquet.NumeroEspacio1);
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            validacion.LimpiarCajasDeTextoYComboBox(this);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}