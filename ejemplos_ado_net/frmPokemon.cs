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


        }
        private void cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                Listapokemons = negocio.listar();
                dgvPokemons.DataSource = Listapokemons;
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["PokemonID"].Visible = false;
                pbxPokemon.Load(Listapokemons[0].UrlImagen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon select = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(select.UrlImagen);
            Elemento elem = new Elemento();
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
        }
    }

