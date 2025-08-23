using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Repaso
{
	public class CuentaAhorro : Cuenta
	{
        public int retirosRealizados;
        public const int LIMITE_SALDO = 1000000;

        public CuentaAhorro() : base()
        {
            retirosRealizados = 0;
        }

        public override void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new DatosInvalidosException("El monto a retirar debe ser mayor a cero.");

            if (saldo >= LIMITE_SALDO)
                throw new LimiteRetirosExcedidoException($"Ha excedido el límite de {LIMITE_RETIROS} retiros por mes.");

            if (monto > saldo)
                throw new FondosInsuficientesException("Fondos insuficientes para realizar el retiro.");

            saldo -= monto;
            retirosRealizados++;
        }

        public override string ObtenerTipoCuenta()
        {
            return "Cuenta de Ahorros";
        }

    }
}
