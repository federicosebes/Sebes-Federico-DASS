using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpRepasoBanco
{
    internal class RCuenta
    {
        private List<Cuenta> cuentas;
        private List<Cliente> clientes;
        public RCuenta()
        {
            cuentas = new List<Cuenta>();
            clientes = new List<Cliente>();
        }
        public void AgregarCuenta(Cuenta cuenta)
        {
            if (cuenta == null) throw new DatosInvalidosException("La cuenta no puede ser nula.");
            if (string.IsNullOrWhiteSpace(cuenta.Ncuenta))
                throw new DatosInvalidosException("El número de cuenta no puede estar vacío.");
            if (ExisteCuenta(cuenta.Ncuenta))
                throw new CuentaDuplicadaException("Ya existe una cuenta con ese número.");

            if (string.IsNullOrWhiteSpace(cuenta.DniTitular))
                throw new DatosInvalidosException("La cuenta debe tener un DNI de titular.");
            if (BuscarCliente(cuenta.DniTitular) == null)
                throw new DatosInvalidosException("El titular no está registrado como cliente.");

            cuentas.Add(cuenta);
        }
        public void AgregarCuenta(Cuenta cuenta, Cliente cliente)
        {
            if (cliente == null) throw new DatosInvalidosException("Debe indicar el titular.");
            cuenta.DniTitular = cliente.Dni; // Tomo el DNI y asocio
            AgregarCuenta(cuenta);
        }

        public Cuenta BuscarCuenta(string numeroCuenta)
        {
            return cuentas.FirstOrDefault(c => c.Ncuenta == numeroCuenta);
        }

        public bool ExisteCuenta(string numeroCuenta)
        {
            return cuentas.Any(c => c.Ncuenta == numeroCuenta);
        }

        public List<Cuenta> ObtenerTodasLasCuentas()
        {
            return cuentas.ToList();
        }

        public void EliminarCuenta(string numeroCuenta)
        {
            var cuenta = BuscarCuenta(numeroCuenta);
            if (cuenta != null)
            {
                cuentas.Remove(cuenta);
            }
        }

        public void AgregarCliente(Cliente cliente)
        {
            if (cliente == null) throw new DatosInvalidosException("Debe existir el cliente.");

            if (string.IsNullOrWhiteSpace(cliente.Dni))
                throw new DatosInvalidosException("El DNI no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.Titular))
                throw new DatosInvalidosException("El nombre y apellido no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.Ntelefono))
                throw new DatosInvalidosException("El número de teléfono no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.Mail))
                throw new DatosInvalidosException("El mail no puede estar vacío.");
            if (cliente.Fechanacimiento == default)
                throw new DatosInvalidosException("La fecha de nacimiento es obligatoria.");

            if (BuscarCliente(cliente.Dni) != null)
                throw new DniDuplicadoException("Ya existe un cliente con ese DNI.");

            clientes.Add(cliente);
        }


        public void EliminarCliente(string dni)
        {
            var cliente = BuscarCliente(dni);
            if (cliente == null) return;

            if (cuentas.Any(c => c.DniTitular == dni))
                throw new DatosInvalidosException("No se puede eliminar: el cliente posee cuentas.");

            clientes.Remove(cliente);
        }

        public void ModificarCliente(Cliente actualizado)
        {
            if (actualizado == null) throw new DatosInvalidosException("Datos de cliente inválidos.");
            var cli = BuscarCliente(actualizado.Dni);
            if (cli == null) throw new DatosInvalidosException("Cliente no encontrado.");

            if (string.IsNullOrWhiteSpace(actualizado.Titular))
                throw new DatosInvalidosException("Nombre y apellido obligatorio.");
            if (string.IsNullOrWhiteSpace(actualizado.Ntelefono))
                throw new DatosInvalidosException("Teléfono obligatorio.");
            if (string.IsNullOrWhiteSpace(actualizado.Mail))
                throw new DatosInvalidosException("Email obligatorio.");
            if (actualizado.Fechanacimiento == default)
                throw new DatosInvalidosException("Fecha de nacimiento obligatoria.");

            cli.Titular = actualizado.Titular;
            cli.Ntelefono = actualizado.Ntelefono;
            cli.Mail = actualizado.Mail;
            cli.Fechanacimiento = actualizado.Fechanacimiento;
        }
        public Cliente BuscarCliente(string dni)
        {
            return clientes.FirstOrDefault(c => c.Dni == dni);
        }

        public List<Cliente> ObtenerTodosLosClientes()
        {
            return clientes.ToList();
        }
    }
}
