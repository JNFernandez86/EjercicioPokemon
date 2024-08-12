using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ejemplos_ado_net
{
    public partial class frmAgregarElementos : Form
    {
        public frmAgregarElementos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ElementoNegocio negocio = new ElementoNegocio();
            Elemento elemento = new Elemento();

            try
            {
                elemento.Descripcion = (string)txtNuevoElemento.Text;
                negocio.agregarElemento(elemento);
                MessageBox.Show("Nuevo Elemento Cargado con éxito");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); 
            }
            Close();
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAgregarElementos_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmElementos frm = new frmElementos();
            
            
        }
    }
}
