﻿using System;
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
            cargarElementos();
        }

        private void cargarElementos()
        {
            ElementoNegocio elem = new ElementoNegocio();

            tipo = elem.listar();
            dgvElementos.DataSource = tipo;
        }
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            frmAgregarElementos frm =new frmAgregarElementos();
            frm.ShowDialog();

            cargarElementos();

        }
    }
}
