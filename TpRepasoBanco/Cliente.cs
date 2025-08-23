using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    public class Cliente
    {
        public string Dni;
        public string Titular;
        public string Ntelefono;
        public string Mail;
        public DateTime Fechanacimiento;


        public int ObtenerEdad()
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - Fechanacimiento.Year;
            if (Fechanacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

    }
}
