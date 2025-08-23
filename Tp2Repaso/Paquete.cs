using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class Paquete
    {
        public string Codigo;
        public string Nombre;
        public string Tipo; // "Premium" | "Silver" | "Basico"
        public List<Canal> Canales;

        public Paquete()
        {
            Canales = new List<Canal>();
            Tipo = "Basico";
        }

        public decimal RecargoPorcentaje()
        {
            // Premium: +20%, Silver: +15%, Básico: 0%
            var t = (Tipo ?? "").Trim().ToLower();
            if (t == "premium") return 0.20m;
            if (t == "silver") return 0.15m;
            return 0m;
        }
    }
}
