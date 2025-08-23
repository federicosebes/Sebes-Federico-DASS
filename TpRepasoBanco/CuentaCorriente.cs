using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    internal class CuentaCorriente : Cuenta
    {
        private decimal limiteCredito; 
        private const decimal LIMITE_SOBREGIRO = 100000m;

        public decimal LimiteCredito
        {
            get { return limiteCredito; }
            set { limiteCredito = value; }
        }

        public CuentaCorriente() : base()
        {
            limiteCredito = LIMITE_SOBREGIRO;
        }

        public override void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new DatosInvalidosException("El monto a retirar debe ser mayor a cero.");

            decimal saldoPosterior = saldo - monto;

            
            if (saldoPosterior < -limiteCredito)
                throw new FondosInsuficientesException(
                    $"Supera el límite de descubierto (${limiteCredito:N2}).");

            saldo = saldoPosterior;
        }

        public decimal ObtenerSaldoDisponible()
        {
            return saldo + limiteCredito;
        }

        public override string ObtenerTipoCuenta()
        {
            return "Cuenta Corriente";
        }




    }
}
