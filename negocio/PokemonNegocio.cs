using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using negocio;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            conexionBD accesodatos = new conexionBD();
            List<Pokemon> lista = new List<Pokemon>();

            try
            {

                accesodatos.setarConsulta("select Numero, Nombre, p.Descripcion, UrlImagen, E.Descripcion Tipo, d.Descripcion Debilidad " +
                    "from POKEMONS p, ELEMENTOS e, elementos d " +
                    "WHERE E.Id = P.IdTipo and d.Id = p.IdDebilidad;");

                accesodatos.ejecutarLectura();


                while (accesodatos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Numero = accesodatos.Lector.GetInt32(0); //metodo 1
                    aux.Nombre = (string)accesodatos.Lector["Nombre"];// metodo 2 - mas usuado -
                    aux.Descripcion = (string)accesodatos.Lector["Descripcion"];
                    aux.UrlImagen = (string)accesodatos.Lector["UrlImagen"];
                    aux.Tipo = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Tipo.Descripcion = (string)accesodatos.Lector["Tipo"];
                    aux.Debilidad = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Debilidad.Descripcion = (string)accesodatos.Lector["Debilidad"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesodatos.cerrarConexion();
            }

        }

        public void agregar(Pokemon nuevo)
        {
            conexionBD datos = new conexionBD();


            try
            {
                datos.setarConsulta("INSERT INTO POKEMONS(Numero,Nombre,Descripcion,UrlImagen,IdTipo,IdDebilidad,Activo) VALUES(" + nuevo.Numero + ",'" + nuevo.Nombre + "', '" + nuevo.Descripcion + "','txt ',1,2,1)");
                datos.ejecutrarAccion();
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
