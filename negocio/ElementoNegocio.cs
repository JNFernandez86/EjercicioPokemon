using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ElementoNegocio
    {
        
        public List<Elemento> listar()
        {
			conexionBD accesodatos = new conexionBD();
            List<Elemento> listaE = new List<Elemento>();

            try
			{
						
				accesodatos.setarConsulta("SELECT Id, Descripcion FROM ELEMENTOS;");
				accesodatos.ejecutarLectura();
				

				while (accesodatos.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.id = (int)accesodatos.Lector["Id"];
					aux.Descripcion = (string)accesodatos.Lector["Descripcion"];

					listaE.Add(aux);
				}
				return listaE;
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
		public void agregarElemento(Elemento TipoandDeb)
		{
			conexionBD db = new conexionBD();

			try
			{
                db.setarConsulta("INSERT INTO ELEMENTOS (DESCRIPCION) VALUES (@Desc);");
				db.setearParametro("@Desc",TipoandDeb.Descripcion);
				db.ejecutarAccion();
				
            }
			catch (Exception ex)
			{

				throw ex;
			}
			
		}
    }
}
