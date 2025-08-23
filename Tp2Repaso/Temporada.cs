using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class Temporada
    {
        public int Numero;
        public List<Episodio> Episodios;

        public Temporada()
        {
            Episodios = new List<Episodio>();
        }
    }
}
