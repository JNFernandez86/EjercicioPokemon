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
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.;database=POKEDEX_DB; integrated security=true";
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "SELECT Numero, Nombre, Descripcion, UrlImagen FROM POKEMONS";
                cmd.CommandText = "select Numero, Nombre, p.Descripcion, UrlImagen, E.Descripcion Tipo, d.Descripcion Debilidad " +
                    "from POKEMONS p, ELEMENTOS e, elementos d " +
                    "WHERE E.Id = P.IdTipo and d.Id = p.IdDebilidad;";
                cmd.Connection = conexion;

                conexion.Open();
                lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0); //metodo 1
                    aux.Nombre = (string)lector["Nombre"];// metodo 2 - mas usuado -
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
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
                conexion.Close();
            }
        }
    }
}
