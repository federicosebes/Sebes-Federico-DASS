using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    public abstract class Cuenta
    {
        public string Ncuenta;
        public decimal saldo;
        public string DniTitular;


        public Cuenta()
        {
            saldo = 0;
        }

        public virtual void Depositar(decimal monto)
        {
            if (monto <= 0)
                throw new DatosInvalidosException("El monto a depositar debe ser mayor a cero.");

            saldo += monto;
        }

        public abstract void Retirar(decimal monto);

        public virtual decimal ConsultarSaldo()
        {
            return saldo;
        }

        public abstract string ObtenerTipoCuenta();





    }
}
