using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ejemplos_ado_net
{
    public partial class frmElementos : Form
    {
        private List<Elemento> tipo;
        public frmElementos()
        {
            InitializeComponent();
        }

        private void frmElementos_Load(object sender, EventArgs e)
        {
            ElementoNegocio elem = new ElementoNegocio();

            tipo = elem.listar();
            dgvElementos.DataSource = tipo;
            

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}
