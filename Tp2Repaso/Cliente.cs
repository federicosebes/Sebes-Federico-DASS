using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class Cliente
    {
        public string CodigoCliente;
        public string Nombre;
        public string Apellido;
        public string Dni;
        public DateTime FechaNacimiento;
        public decimal AbonoBase;
        public string PaqueteCodigo; // null si no contrató paquete

        public int Edad()
        {
            var hoy = DateTime.Today;
            int e = hoy.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > hoy.AddYears(-e)) e--;
            return e;
        }


    }
}
