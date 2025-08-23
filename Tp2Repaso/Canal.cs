using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class Canal
    {
        public string Codigo;
        public string Nombre;
        public List<Serie> Series;

        public Canal()
        {
            Series = new List<Serie>();
        }
    }
}
