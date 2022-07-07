using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoastalParking
{
    public partial class FrmRegistrarEspacio : Form
    {
        FrmLogin login = new FrmLogin();
        ParqueaderoService parqueaderoService;
        public FrmRegistrarEspacio()
        {
            parqueaderoService = new ParqueaderoService(ConfigConnectionString.ConnectionString);
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int valor = parqueaderoService.ConsultarTotal();
            if (login.dato.Equals("admin"))
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
