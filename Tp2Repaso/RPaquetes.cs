using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class RPaquetes
    {
        private List<Paquete> paquetes = new List<Paquete>();

        public void Agregar(Paquete p)
        {
            if (p == null) throw new DatosInvalidosException("Paquete nulo.");
            if (string.IsNullOrWhiteSpace(p.Codigo)) throw new DatosInvalidosException("Código de paquete obligatorio.");
            if (string.IsNullOrWhiteSpace(p.Nombre)) throw new DatosInvalidosException("Nombre de paquete obligatorio.");
            if (BuscarPorCodigo(p.Codigo) != null) throw new CodigoDuplicadoException("Código de paquete duplicado.");
            paquetes.Add(p);
        }

        public Paquete BuscarPorCodigo(string codigo)
        {
            foreach (var p in paquetes) if (p.Codigo == codigo) return p; return null;
        }

        public List<Paquete> ListarTodos() { return new List<Paquete>(paquetes); }
        public void Eliminar(string codigo)
        {
            var p = BuscarPorCodigo(codigo);
            if (p == null) throw new EntidadNoEncontradaException("Paquete no encontrado.");
            paquetes.Remove(p);
        }

    }
}
