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

        private string GetQuery(string a, string criterio, string variablef)
        {
            switch (criterio)
            {

                case "Comienza con":
                    a += " like '" + variablef + "%' ";
                    return a;
                   
                case "Termina con":
                    a += " like '%" + variablef + "' ";
                    return a;
                case "Contiene":
                    a += " like '%" + variablef + "%' ";
                    return a;
                default:
                    return a;
            }
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
                string campoquery = "";
                switch (campo)
                {
                    case "Número":
                        campoquery = "numero ";
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += campoquery +" > ";
                                break;
                            case "Menor a":
                                consulta += campoquery +" < ";
                                break;
                            case "Igual a":
                                consulta += campoquery + "= ";
                                break;
                            default:
                                break;
                        }
                        consulta += filtro;
                        break;

                    case "Nombre":
                        consulta += GetQuery("Nombre",criterio,filtro);
                        break ;
                    case "Descripcion":
                        consulta += GetQuery("p.descripcion", criterio, filtro);
                        break ;
                    case "Debilidad":
                        consulta += GetQuery("Debilidad", criterio, filtro);
                        break;
                    case "Tipo":
                        consulta += GetQuery("Tipo", criterio, filtro);
                        break;
                    default :
                        break;

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
