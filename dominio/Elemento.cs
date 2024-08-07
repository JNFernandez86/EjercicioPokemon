using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Elemento
    {
        public int id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString() // se sobrescribe el metodo to string para que te muestre el texto en vez de los elementos del objeto
        {
            return Descripcion;
        }
    }
}

