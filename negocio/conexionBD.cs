using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class conexionBD
    {
        private SqlConnection conexion;
        private SqlCommand cmd;
        private SqlDataReader da;

        public SqlDataReader Lector 
        {
            get { return da; }
        }

        public conexionBD()
        {
            conexion = new SqlConnection("server=.;database=POKEDEX_DB; integrated security=true");
            cmd = new SqlCommand();


        }
        public void setarConsulta(string consulta)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            
        }

        public void ejecutarLectura()
        {
            try
            {
                cmd.Connection = conexion;
                conexion.Open();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void cerrarConexion()
        {
            if(Lector != null)
            conexion.Close();
        }
    }
}
