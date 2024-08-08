using dominio;
using System;
using System.Collections.Generic;
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
						
				accesodatos.setarConsulta("SELECT * FROM ELEMENTOS;");
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
    }
}
