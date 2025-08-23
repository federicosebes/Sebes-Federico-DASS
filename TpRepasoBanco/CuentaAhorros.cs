using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    internal class CuentaAhorros : Cuenta
    {
        public decimal MontoMaximoPorExtraccion;

        public CuentaAhorros() : base()
        {
            MontoMaximoPorExtraccion = 50000m;
        }

        public override void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new DatosInvalidosException("El monto a retirar debe ser mayor a cero.");

            if (monto > MontoMaximoPorExtraccion)
                throw new LimiteRetirosExcedidoException(
                    $"Supera el máximo por extracción: ${MontoMaximoPorExtraccion:N2}");

            if (monto > saldo)
                throw new FondosInsuficientesException("Fondos insuficientes para realizar el retiro.");

            saldo -= monto;
        }

        public override string ObtenerTipoCuenta()
        {
            return "Cuenta de Ahorros";
        }

    }
}
