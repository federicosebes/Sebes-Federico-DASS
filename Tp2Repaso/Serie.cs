using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class Serie
    {
        public string Nombre;
        public int CantidadTemporadas; // útil para informes
        public string Genero;
        public string Director;
        public decimal Ranking;        // ej: 0..5
        public List<Temporada> Temporadas;

        public Serie()
        {
            Temporadas = new List<Temporada>();
        }
    }
}
