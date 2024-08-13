using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using negocio;
using System.Security.AccessControl;

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

                accesodatos.setarConsulta("select Numero, Nombre, p.Descripcion, UrlImagen, E.Descripcion Tipo, d.Descripcion Debilidad,p.IdTipo,p.IdDebilidad, p.id " +
                    "from POKEMONS p, ELEMENTOS e, elementos d " +
                    "WHERE E.Id = P.IdTipo and d.Id = p.IdDebilidad" +
                    " ORDER BY Numero;");

                accesodatos.ejecutarLectura();


                while (accesodatos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.PokemonID = (int)accesodatos.Lector["Id"];
                    aux.Numero = accesodatos.Lector.GetInt32(0); //metodo 1
                    aux.Nombre = (string)accesodatos.Lector["Nombre"];// metodo 2 - mas usuado -
                    aux.Descripcion = (string)accesodatos.Lector["Descripcion"];
                   
                    // primer metodo

                    //if(!(accesodatos.Lector.IsDBNull(accesodatos.Lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)accesodatos.Lector["UrlImagen"];

                    //metodo 2

                    if (!(accesodatos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)accesodatos.Lector["UrlImagen"];

                    //------
                    aux.Tipo = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Tipo.Id = (int)accesodatos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)accesodatos.Lector["Tipo"];
                    aux.Debilidad = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Debilidad.Id = (int)accesodatos.Lector["IdDebilidad"];
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
                datos.setarConsulta("INSERT INTO POKEMONS(Numero,Nombre,Descripcion,UrlImagen,IdTipo,IdDebilidad,Activo) VALUES" +
                    "(" + nuevo.Numero + ",'" + nuevo.Nombre + "', '" + nuevo.Descripcion + "',@UrlImagen,@idTipo,@idDebilidad,1)");

                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("@UrlImagen", nuevo.UrlImagen);
                datos.ejecutrarAccion();
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void modificar(Pokemon poke)
        {
            conexionBD datos = new conexionBD();

            try
            {
                datos.setarConsulta("UPDATE POKEMONS SET Numero=@numero,Nombre=@nombre,Descripcion=@desc,UrlImagen=@UrlImagen,IdTipo=@idtipo,IdDebilidad=@iddebilidad where Id=@idpoke");
                datos.setearParametro("@numero", poke.Numero);
                datos.setearParametro("@nombre", poke.Nombre);
                datos.setearParametro("@desc", poke.Descripcion);
                datos.setearParametro("@UrlImagen", poke.UrlImagen);
                datos.setearParametro("@idtipo", poke.Tipo.Id);
                datos.setearParametro("@iddebilidad", poke.Debilidad.Id);
                datos.setearParametro("@idpoke", poke.PokemonID);


                datos.ejecutarAccion();

                

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }

}
