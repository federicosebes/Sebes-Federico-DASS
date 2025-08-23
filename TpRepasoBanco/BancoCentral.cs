using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    public class BancoCentral
    {
        private RCuenta repositorio;
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public BancoCentral()
        {
            repositorio = new RCuenta();
            nombre = "Banco Nacional";
        }
        public Cliente BuscarCliente(string dni)
        {
            return repositorio.BuscarCliente(dni);
        }
        public List<Cliente> ObtenerTodosLosClientes()
        {
            return repositorio.ObtenerTodosLosClientes();
        }
        public void ModificarCliente(Cliente actualizado)
        {
            repositorio.ModificarCliente(actualizado);
        }

        public void CrearCliente(string dni, string titular, string telefono, string mail, DateTime fechaNacimiento)
        {
            var cliente = new Cliente
            {
                Dni = dni,
                Titular = titular,
                Ntelefono = telefono,
                Mail = mail,
                Fechanacimiento = fechaNacimiento
            };

            repositorio.AgregarCliente(cliente);
        }

        public void EliminarCliente(string dni)
        {
            var cliente = repositorio.BuscarCliente(dni);
            if (cliente == null)
                throw new DatosInvalidosException("Cliente no encontrado.");

            bool tieneCuentas = repositorio.ObtenerTodasLasCuentas()
                                           .Any(c => c.DniTitular == dni);
            if (tieneCuentas)
                throw new DatosInvalidosException("No se puede eliminar: el cliente posee cuentas.");

            repositorio.EliminarCliente(dni);
        }



        public void CrearCuentaAhorros(string numeroCuenta, string dniTitular)
        {
            var cliente = repositorio.BuscarCliente(dniTitular);
            if (cliente == null)
                throw new DatosInvalidosException("El titular debe estar registrado como cliente.");

            var cuenta = new CuentaAhorros();
            cuenta.Ncuenta = numeroCuenta;
            cuenta.DniTitular = dniTitular;
            

            repositorio.AgregarCuenta(cuenta);
        }

        public void CrearCuentaCorriente(string numeroCuenta, string dniTitular)
        {
            var cliente = repositorio.BuscarCliente(dniTitular);
            if (cliente == null)
                throw new DatosInvalidosException("El titular debe estar registrado como cliente.");

            var cuenta = new CuentaCorriente();
            cuenta.Ncuenta = numeroCuenta;
            cuenta.DniTitular = dniTitular;
            

            repositorio.AgregarCuenta(cuenta);
        }

        public void EliminarCuenta(string numeroCuenta)
        {
            var cuenta = repositorio.BuscarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new DatosInvalidosException("Cuenta no encontrada.");

            if (cuenta.saldo != 0)
                throw new DatosInvalidosException("No se puede eliminar la cuenta: el saldo debe ser 0.");

            repositorio.EliminarCuenta(numeroCuenta);
        }

        public void CambiarTitularCuenta(string numeroCuenta, string nuevoDniTitular)
        {
            var cuenta = repositorio.BuscarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new DatosInvalidosException("Cuenta no encontrada.");

            var nuevo = repositorio.BuscarCliente(nuevoDniTitular);
            if (nuevo == null)
                throw new DatosInvalidosException("El nuevo titular no está registrado.");

            cuenta.DniTitular = nuevoDniTitular;
        }

        public void RealizarDeposito(string numeroCuenta, decimal monto)
        {
            var cuenta = repositorio.BuscarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new DatosInvalidosException("Cuenta no encontrada.");

            cuenta.Depositar(monto);
        }

        public void RealizarRetiro(string numeroCuenta, decimal monto)
        {
            var cuenta = repositorio.BuscarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new DatosInvalidosException("Cuenta no encontrada.");

            cuenta.Retirar(monto);
        }

        public decimal ConsultarSaldo(string numeroCuenta)
        {
            var cuenta = repositorio.BuscarCuenta(numeroCuenta);
            if (cuenta == null)
                throw new DatosInvalidosException("Cuenta no encontrada.");

            return cuenta.ConsultarSaldo();
        }

        public Cuenta ObtenerCuenta(string numeroCuenta)
        {
            return repositorio.BuscarCuenta(numeroCuenta);
        }

        public List<Cuenta> ObtenerTodasLasCuentas()
        {
            return repositorio.ObtenerTodasLasCuentas();
        }

    }
}
