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
    public partial class frmPokemon : Form
    {
        private List<Pokemon> Listapokemons;
        Pokemon seleccionado;
        public frmPokemon()
        {
            InitializeComponent();
        }
      
        private void frmPokemon_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Número");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");

        }
        private void cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                Listapokemons = negocio.listar();
                dgvPokemons.DataSource = Listapokemons;
                OcultarColumnas();
                pbxPokemon.Load(Listapokemons[0].UrlImagen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Elemento elem = new Elemento();
            if (dgvPokemons.CurrentRow != null)
            {
                Pokemon select = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                cargarImagen(select.UrlImagen);
            }
            
            
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxPokemon.Load("https://static.thenounproject.com/png/261694-200.png");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            cargar();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            frmAltaPokemon alter = new frmAltaPokemon(seleccionado);
            alter.ShowDialog();
            cargar();
        }

        private void OcultarColumnas() 
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["PokemonID"].Visible = false;
        }
             
        //Eliminacion Fisica elimina los datos de la base de datos (irrecuperables)
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void eliminar(bool logico = false)
        {
                PokemonNegocio negocio = new PokemonNegocio();
                try
                {

                    DialogResult respuesta = MessageBox.Show("Esta por eliminar un registro de la Base de Datos, ¿Esta seguro de eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.Yes)
                    {
                        seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

                    if (logico)
                    {
                        negocio.eliminarLogico(seleccionado.PokemonID);
                    }
                    else
                    {
                        negocio.eliminar(seleccionado.PokemonID);
                    }
                        
                        cargar();
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }

        

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtfiltroAvanzado.Text;
                dgvPokemons.DataSource = negocio.filtrar(campo, criterio,filtro);
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listaFiltrada;

            string filtro = txtFiltro.Text;


            if (filtro != "")
            {
                listaFiltrada = Listapokemons.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = Listapokemons;
            }
            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;
            OcultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
           string opcion = cboCampo.SelectedItem.ToString();

            if(opcion == "Número")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");

            }
        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }

