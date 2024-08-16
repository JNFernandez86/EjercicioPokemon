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
using System.IO;
using System.Configuration;

namespace ejemplos_ado_net
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        private OpenFileDialog archivo = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }
        public frmAltaPokemon(Pokemon poke)
        {
            InitializeComponent();
            this.pokemon = poke;
            Text = "Modificar Pokemon";
            btnAceptar.Text = "Modificar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
            PokemonNegocio negocio = new PokemonNegocio();
         
            try
            {
                if(pokemon == null)
                    pokemon = new Pokemon();
               
                pokemon.Numero = int.Parse(txtNumeroPokemon.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem;
                pokemon.UrlImagen = txtUrlImagen.Text;
                

                if(pokemon.PokemonID != 0)
                {
                    //.Text = (ConfigurationManager.AppSettings["Imagenes"] + archivo.SafeFileName);
                    negocio.modificar(pokemon);
                    MessageBox.Show("Pokemon Modificado exitosamente");
                }
                else
                {
                    //txtUrlImagen.Text = (ConfigurationManager.AppSettings["Imagenes"] + archivo.SafeFileName);
                    negocio.agregar(pokemon);
                    MessageBox.Show("Pokemon agregado exitosamente");
                }

                // guardo imagen si la levanto localmente
                if (archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                {
                    if (File.Exists(archivo.SafeFileName))
                    {
                        MessageBox.Show("Archivo ya existente","Archivo duplicado",MessageBoxButtons.OKCancel);

                        //if(MessageBoxButtons.OK == true)
                        
                    }
                    else
                    {
                        File.Copy(archivo.FileName, ConfigurationManager.AppSettings["Imagenes"] + archivo.SafeFileName);
                    }
                    
                    
                }

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNeg = new ElementoNegocio();

            try
            {
                cboTipo.DataSource = elementoNeg.listar();
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion";
                cboDebilidad.DataSource = elementoNeg.listar();
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion";

                if (pokemon != null)
                {
                    txtNumeroPokemon.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxImagen.Load("https://static.thenounproject.com/png/261694-200.png");
            }

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg|png|*.png";

            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text= archivo.FileName;
                cargarImagen(archivo.FileName);

                txtUrlImagen.Text = (ConfigurationManager.AppSettings["Imagenes"] + archivo.SafeFileName);
                //GUARDAR LA IMAGEN
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["Imagenes"] + archivo.SafeFileName);
            }
        }
    }
}
