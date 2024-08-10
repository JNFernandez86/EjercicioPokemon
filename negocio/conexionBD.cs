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
        private SqlDataReader lector;

        public SqlDataReader Lector 
        {
            get { return lector; }
        }

        public conexionBD()
        {
            conexion = new SqlConnection("Data Source=192.168.0.13;Initial Catalog=POKEDEX_DB; User ID=Administrador;Password=Soporte00");
            //conexion = new SqlConnection("server=DESKTOP-8628R8A;database=POKEDEX_DB; integrated security=true");
            cmd = new SqlCommand();


        }
        public void setarConsulta(string consulta)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            
        }

        public void ejecutarLectura()
        { 
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                lector = cmd.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void ejecutrarAccion()
        { 
            cmd.Connection=conexion;
            try
            {

                conexion.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        public void cerrarConexion()
        {
            if(Lector != null)
            conexion.Close();
        }
    }
}
