﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;

namespace ejemplos_ado_net
{
    public partial class frmElementos : Form
    {
        public frmElementos()
        {
            InitializeComponent();
        }

        private void frmElementos_Load(object sender, EventArgs e)
        {
            ElementoNegocio elem = new ElementoNegocio();

            dgvElementos.DataSource= elem.listar();
           
        }
    }
}
