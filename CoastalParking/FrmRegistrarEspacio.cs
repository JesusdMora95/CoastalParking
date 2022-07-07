using BLL;
using Entity;
using System;
using System.Windows.Forms;

namespace CoastalParking
{
    public partial class FrmRegistrarEspacio : Form
    {
        ParqueaderoService parqueaderoService;
        EstacionamientoService estacionamientoService;
        public FrmRegistrarEspacio()
        {
            parqueaderoService = new ParqueaderoService(ConfigConnectionString.ConnectionString);
            estacionamientoService = new EstacionamientoService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int valor = parqueaderoService.ConsultarTotal();
            if (FrmLogin.dato.Equals("admin"))
            {
                MessageBox.Show(parqueaderoService.Modificar(ConstruirParqueadero().EspacioTotal, ConstruirParqueadero().EspacioDisponible, valor));
            }
            else
            {
                MessageBox.Show("Usted No Esta Autorizado Para Modificar Esta Función");
            }
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(parqueaderoService.Guardar(ConstruirParqueadero().EspacioTotal, ConstruirParqueadero().EspacioDisponible));
            GuardarEstacionamiento();
        }

        private void GuardarEstacionamiento()
        {
            for (int i = 1; i <= Convert.ToInt32(txtNumeroEspacio.Text); i++)
            {
                estacionamientoService.Guardar(CrearEstacionamiento(i));
            }
        }

        private Estacionamiento CrearEstacionamiento(int valor)
        {
            Estacionamiento estacionamiento = new Estacionamiento(valor);
            return estacionamiento;
        }

        private Parqueadero ConstruirParqueadero()
        {
            Parqueadero parqueadero = new Parqueadero();
            parqueadero.EspacioTotal = Convert.ToInt32(txtNumeroEspacio.Text);
            parqueadero.EspacioDisponible = 0;
            return parqueadero;
        }

        private void btBorrar_Click(object sender, EventArgs e)
        {
            txtNumeroEspacio.Text = "";
        }
    }
}