using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class RCliente
    {
        private List<Cliente> clientes = new List<Cliente>();

        public void Agregar(Cliente c)
        {
            if (c == null) throw new DatosInvalidosException("Cliente nulo.");
            if (string.IsNullOrWhiteSpace(c.CodigoCliente)) throw new DatosInvalidosException("Código de cliente obligatorio.");
            if (string.IsNullOrWhiteSpace(c.Nombre)) throw new DatosInvalidosException("Nombre obligatorio.");
            if (string.IsNullOrWhiteSpace(c.Apellido)) throw new DatosInvalidosException("Apellido obligatorio.");
            if (string.IsNullOrWhiteSpace(c.Dni)) throw new DatosInvalidosException("DNI obligatorio.");

            if (BuscarPorCodigo(c.CodigoCliente) != null) throw new CodigoDuplicadoException("Código de cliente duplicado.");
            if (BuscarPorDni(c.Dni) != null) throw new DniDuplicadoException("Ya existe un cliente con ese DNI.");
            clientes.Add(c);
        }

        public void Modificar(Cliente actualizado)
        {
            var ex = BuscarPorCodigo(actualizado.CodigoCliente);
            if (ex == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            if (string.IsNullOrWhiteSpace(actualizado.Nombre)) throw new DatosInvalidosException("Nombre obligatorio.");
            if (string.IsNullOrWhiteSpace(actualizado.Apellido)) throw new DatosInvalidosException("Apellido obligatorio.");
            if (string.IsNullOrWhiteSpace(actualizado.Dni)) throw new DatosInvalidosException("DNI obligatorio.");
            // Validar DNI único si cambió
            var otro = BuscarPorDni(actualizado.Dni);
            if (otro != null && !ReferenceEquals(otro, ex)) throw new DniDuplicadoException("DNI ya utilizado por otro cliente.");

            ex.Nombre = actualizado.Nombre;
            ex.Apellido = actualizado.Apellido;
            ex.Dni = actualizado.Dni;
            ex.FechaNacimiento = actualizado.FechaNacimiento;
            ex.AbonoBase = actualizado.AbonoBase;
        }

        public void Eliminar(string codigoCliente)
        {
            var c = BuscarPorCodigo(codigoCliente);
            if (c == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            clientes.Remove(c);
        }

        public Cliente BuscarPorCodigo(string codigo)
        {
            foreach (var c in clientes) if (c.CodigoCliente == codigo) return c; return null;
        }

        public Cliente BuscarPorDni(string dni)
        {
            foreach (var c in clientes) if (c.Dni == dni) return c; return null;
        }

        public List<Cliente> BuscarPorNombre(string nombre)
        {
            List<Cliente> lista = new List<Cliente>();
            var n = (nombre ?? "").ToLower();
            foreach (var c in clientes)
            {
                string comp = (c.Nombre + " " + c.Apellido).ToLower();
                if (comp.Contains(n)) lista.Add(c);
            }
            return lista;
        }

        public List<Cliente> ListarTodos() { return new List<Cliente>(clientes); }


    }
}
