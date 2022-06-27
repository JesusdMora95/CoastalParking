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
        TarifaService tarifaService;
        public FrmRegistrarTicketAlquiler()
        {
            InitializeComponent();
            vehiculoService = new VehiculoService(ConfigConnectionString.ConnectionString);
            tiquetParticularService = new TiquetParticularService(ConfigConnectionString.ConnectionString);
            tarifaService = new TarifaService(ConfigConnectionString.ConnectionString);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (vehiculoService.Buscar(txtPlaca.Text).Error)
            {
                Vehiculo vehiculo = MapearVehiculo();
                string mensajeVehiculo = vehiculoService.Guardar(vehiculo);
            }
            else
            {
                if (vehiculoService.BuscaporPlaca(txtPlaca.Text).Equals("NO"))
                {
                    tarifaService.ConsultaUltimoTiquet(cmbTipoVehiculo.Text);
                    TiquetParticular tiquetParticular = MapearTiquetParticular(tarifaService.ConsultaUltimoTiquet(cmbTipoVehiculo.Text).Tarifa);
                    string mensaje = tiquetParticularService.Guardar(tiquetParticular);
                    MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private TiquetParticular MapearTiquetParticular(Tarifa tarifa)
        {
            TiquetParticular tiquetParticular = new TiquetParticular();
            tiquetParticular.Codigo = ConsecutivoTiquet();
            tiquetParticular.HoraEntrada = DateTime.Now;
            tiquetParticular.HoraSalida = DateTime.Now;
            tiquetParticular.EstadoTiquet = false;
            tiquetParticular.ValorExtra = 0;
            tiquetParticular.ValorTotal = 0;
            tiquetParticular.ValorMinimoTarifa = tarifa.ValorMinimo;
            tiquetParticular.Tipo = Convert.ToString(tarifa.TipoVehiculo);
            tiquetParticular.Placa = txtPlaca.Text;
            tiquetParticular.NumeroEspacio = 1;
            return tiquetParticular;
        }

        private string ConsecutivoTiquet()
        {
            if (tiquetParticularService.BuscarCodigo().Mensaje != null)
            {
                return "1";
            }
            else
            {
                return Convert.ToString(Convert.ToInt32(tiquetParticularService.BuscarCodigo().TiquetParticular.Codigo) + 1);
            }
        }

        private Vehiculo MapearVehiculo()
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Placa = txtPlaca.Text;
            vehiculo.Tipo = cmbTipoVehiculo.Text + "";
            vehiculo.ModeloyMarca = txtModelo.Text + ";" + txtMarca.Text;
            vehiculo.AplicaConvenio = "NO";
            return vehiculo;
        }

        private void Guardar()
        {

        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            BuscarPlaca();
        }

        void BuscarPlaca()
        {
            char delimiter = ';';
            string placa = txtPlaca.Text;

            var buscarPlaca = vehiculoService.Buscar(placa);

            if (buscarPlaca.Vehiculo == null)
            {
                MessageBox.Show("EL VEHICULO NO EXISTE, POR VUELVA A INTENTAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmbTipoVehiculo.Text = buscarPlaca.Vehiculo.Tipo;
                string[] Datos = buscarPlaca.Vehiculo.ModeloyMarca.Split(delimiter);
                txtModelo.Text = Datos[0];
                txtMarca.Text = Datos[1];
                MessageBox.Show("EL VEHICULO SE ENCONTRO CORRECTAMENTE ", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}