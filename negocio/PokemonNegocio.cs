using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using negocio;
using System.Security.AccessControl;
using System.Collections;

namespace negocio
{
    public class PokemonNegocio
    {
        
        public List<Pokemon> listar()
        {
            conexionBD datos = new conexionBD();
            List<Pokemon> lista = new List<Pokemon>();

            try
            {

                datos.setarConsulta("select Numero, Nombre, p.Descripcion, UrlImagen, E.Descripcion Tipo, d.Descripcion Debilidad,p.IdTipo,p.IdDebilidad, p.id " +
                    "from POKEMONS p, ELEMENTOS e, elementos d " +
                    "WHERE E.Id = P.IdTipo and d.Id = p.IdDebilidad and p.activo=1" +
                    " ORDER BY Numero;");

                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.PokemonID = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0); //metodo 1
                    aux.Nombre = (string)datos.Lector["Nombre"];// metodo 2 - mas usuado -
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                   
                    // primer metodo

                    //if(!(accesodatos.Lector.IsDBNull(accesodatos.Lector.GetOrdinal("UrlImagen"))))
                    //    aux.UrlImagen = (string)accesodatos.Lector["UrlImagen"];

                    //metodo 2

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    //------
                    aux.Tipo = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento(); // sin esto puede arrojar existencia Nula
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

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
                datos.cerrarConexion();
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
        public void eliminar(int id)
        {
            conexionBD datos =new conexionBD();

            try
            {
                datos.setarConsulta("DELETE FROM POKEMONS WHERE Id=@idpoke");
                datos.setearParametro("idpoke",id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion();}


        }

        public void eliminarLogico(int id)
        {
            conexionBD datos = new conexionBD();

            try
            {
                datos.setarConsulta("UPDATE POKEMONS SET Activo = 0 WHERE Id=@idpoke");
                datos.setearParametro("idpoke", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }


        }
        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
            List<Pokemon> list = new List<Pokemon>();
            conexionBD datos = new conexionBD();

            try
            {
                string consulta = ("select Numero, Nombre, p.Descripcion, UrlImagen, E.Descripcion Tipo, d.Descripcion Debilidad,p.IdTipo,p.IdDebilidad, p.id " +
                    "from POKEMONS p, ELEMENTOS e, elementos d " +
                    "WHERE E.Id = P.IdTipo and d.Id = p.IdDebilidad and p.activo=1 and ");
                    //" ORDER BY Numero;");

                if(campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        case "Igual a":
                            consulta += "Numero = " + filtro;
                            break;
                        default:
                            break;
                    }
                }
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '"+filtro+ "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "' ";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%' ";
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "d.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "d.Descripcion like '%" + filtro + "' ";
                            break;
                        case "Contiene":
                            consulta += "d.Descripcion like '%" + filtro + "%' ";
                            break;
                        default:
                            break;
                    }
                }
                datos.setarConsulta(consulta);
                datos.ejecutarLectura();
                
                    while (datos.Lector.Read())
                    {
                        Pokemon aux = new Pokemon();

                        aux.PokemonID = (int)datos.Lector["Id"];
                        aux.Numero = datos.Lector.GetInt32(0);
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                        aux.Tipo = new Elemento();
                        aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                        aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                        aux.Debilidad = new Elemento(); // sin esto puede arrojar existencia Nula
                        aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                        aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                        list.Add(aux);
                    }

                return list;
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
