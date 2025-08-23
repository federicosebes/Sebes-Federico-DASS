using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Repaso
{
    public class CentroServicio
    {
        private RCliente rClientes;
        private RPaquetes rPaquetes;

        public RCliente Clientes { get { return rClientes; } }
        public RPaquetes Paquetes { get { return rPaquetes; } }

        public CentralCable()
        {
            rClientes = new RCliente();
            rPaquetes = new RPaquetes();
        }

        
        public void CrearCliente(string codigoCliente, string nombre, string apellido, string dni, DateTime fechaNacimiento, decimal abonoBase)
        {
            Cliente c = new Cliente();
            c.CodigoCliente = codigoCliente;
            c.Nombre = nombre;
            c.Apellido = apellido;
            c.Dni = dni;
            c.FechaNacimiento = fechaNacimiento;
            c.AbonoBase = abonoBase;
            rClientes.Agregar(c);
        }

        public void ModificarCliente(Cliente actualizado)
        {
            rClientes.Modificar(actualizado);
        }

       
        public void EliminarCliente(string codigoCliente)
        {
            var c = rClientes.BuscarPorCodigo(codigoCliente);
            if (c == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            if (!string.IsNullOrWhiteSpace(c.PaqueteCodigo)) throw new DatosInvalidosException("No se puede eliminar: el cliente tiene un paquete contratado.");
            rClientes.Eliminar(codigoCliente);
        }

        public Cliente BuscarClientePorCodigo(string codigo) { return rClientes.BuscarPorCodigo(codigo); }
        public Cliente BuscarClientePorDni(string dni) { return rClientes.BuscarPorDni(dni); }
        public List<Cliente> ListarClientes() { return rClientes.ListarTodos(); }
        public List<Cliente> BuscarClientesPorNombre(string nombre) { return rClientes.BuscarPorNombre(nombre); }

        
        public void CrearPaquete(string codigo, string nombre, string tipo)
        {
            Paquete p = new Paquete();
            p.Codigo = codigo;
            p.Nombre = nombre;
            p.Tipo = tipo; 
            rPaquetes.Agregar(p);
        }

        public Paquete BuscarPaquete(string codigo) { return rPaquetes.BuscarPorCodigo(codigo); }
        public List<Paquete> ListarPaquetes() { return rPaquetes.ListarTodos(); }

        public void EliminarPaquete(string codigo)
        {
            
            foreach (var cl in rClientes.ListarTodos())
            {
                if (cl.PaqueteCodigo == codigo) throw new DatosInvalidosException("No se puede eliminar: hay clientes con este paquete.");
            }
            rPaquetes.Eliminar(codigo);
        }

       
        public void AgregarCanalAPaquete(string codigoPaquete, string codigoCanal, string nombreCanal)
        {
            var p = BuscarPaqueteObligatorio(codigoPaquete);
            if (BuscarCanal(p, codigoCanal) != null) throw new CodigoDuplicadoException("Código de canal duplicado en el paquete.");
            Canal canal = new Canal();
            canal.Codigo = codigoCanal;
            canal.Nombre = nombreCanal;
            p.Canales.Add(canal);
        }

        public void AgregarSerieACanal(string codigoPaquete, string codigoCanal, string nombreSerie, string genero, string director, decimal ranking)
        {
            var canal = BuscarCanalObligatorio(codigoPaquete, codigoCanal);
            if (BuscarSerie(canal, nombreSerie) != null) throw new CodigoDuplicadoException("Ya existe una serie con ese nombre en el canal.");
            Serie s = new Serie();
            s.Nombre = nombreSerie;
            s.Genero = genero;
            s.Director = director;
            s.Ranking = ranking;
            canal.Series.Add(s);
            s.CantidadTemporadas = 0;
        }

        public void AgregarTemporadaASerie(string codigoPaquete, string codigoCanal, string nombreSerie, int numeroTemporada)
        {
            var serie = BuscarSerieObligatoria(codigoPaquete, codigoCanal, nombreSerie);
            if (BuscarTemporada(serie, numeroTemporada) != null) throw new CodigoDuplicadoException("Ya existe esa temporada.");
            Temporada t = new Temporada();
            t.Numero = numeroTemporada;
            serie.Temporadas.Add(t);
            serie.CantidadTemporadas = serie.Temporadas.Count;
        }

        public void AgregarEpisodioATemporada(string codigoPaquete, string codigoCanal, string nombreSerie, int numeroTemporada, string nombreEpisodio, int duracionMin)
        {
            var temp = BuscarTemporadaObligatoria(codigoPaquete, codigoCanal, nombreSerie, numeroTemporada);
            if (BuscarEpisodio(temp, nombreEpisodio) != null) throw new CodigoDuplicadoException("Ya existe un episodio con ese nombre en la temporada.");
            Episodio e = new Episodio();
            e.Nombre = nombreEpisodio;
            e.DuracionMinutos = duracionMin;
            temp.Episodios.Add(e);
        }

        
        public void ContratarPaquete(string codigoCliente, string codigoPaquete)
        {
            var cl = rClientes.BuscarPorCodigo(codigoCliente);
            if (cl == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            var p = rPaquetes.BuscarPorCodigo(codigoPaquete);
            if (p == null) throw new EntidadNoEncontradaException("Paquete no encontrado.");
            if (!string.IsNullOrWhiteSpace(cl.PaqueteCodigo)) throw new DatosInvalidosException("El cliente ya tiene un paquete contratado.");
            cl.PaqueteCodigo = codigoPaquete;
        }

        public void DescontratarPaquete(string codigoCliente)
        {
            var cl = rClientes.BuscarPorCodigo(codigoCliente);
            if (cl == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            cl.PaqueteCodigo = null;
        }

       
        public decimal CalcularImporteMensualCliente(string codigoCliente)
        {
            var cl = rClientes.BuscarPorCodigo(codigoCliente);
            if (cl == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            decimal recargo = 0m;
            if (!string.IsNullOrWhiteSpace(cl.PaqueteCodigo))
            {
                var p = rPaquetes.BuscarPorCodigo(cl.PaqueteCodigo);
                if (p != null) recargo = p.RecargoPorcentaje();
            }
            return cl.AbonoBase * (1 + recargo);
        }

        public decimal TotalRecaudadoMensual()
        {
            decimal total = 0m;
            var lista = rClientes.ListarTodos();
            foreach (var cl in lista)
            {
                total += CalcularImporteMensualCliente(cl.CodigoCliente);
            }
            return total;
        }

        public Paquete PaqueteMasVendido()
        {
            
            Dictionary<string, int> contador = new Dictionary<string, int>();
            foreach (var cl in rClientes.ListarTodos())
            {
                if (!string.IsNullOrWhiteSpace(cl.PaqueteCodigo))
                {
                    if (!contador.ContainsKey(cl.PaqueteCodigo)) contador[cl.PaqueteCodigo] = 0;
                    contador[cl.PaqueteCodigo]++;
                }
            }
            string topCodigo = null; int top = -1;
            foreach (var kv in contador)
            {
                if (kv.Value > top) { top = kv.Value; topCodigo = kv.Key; }
            }
            if (topCodigo == null) return null;
            return rPaquetes.BuscarPorCodigo(topCodigo);
        }

        public List<Serie> SeriesConRankingMayorA(decimal minimo)
        {
            List<Serie> res = new List<Serie>();
            foreach (var p in rPaquetes.ListarTodos())
            {
                foreach (var canal in p.Canales)
                {
                    foreach (var s in canal.Series)
                    {
                        if (s.Ranking > minimo) res.Add(s);
                    }
                }
            }
            return res;
        }

        
        public string DescribirPaquete(string codigoPaquete)
        {
            var p = rPaquetes.BuscarPorCodigo(codigoPaquete);
            if (p == null) throw new EntidadNoEncontradaException("Paquete no encontrado.");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"Paquete {p.Codigo} - {p.Nombre} ({p.Tipo})");
            foreach (var canal in p.Canales)
            {
                sb.AppendLine($"  Canal {canal.Codigo} - {canal.Nombre}");
                foreach (var s in canal.Series)
                {
                    sb.AppendLine($"    Serie: {s.Nombre} | Género: {s.Genero} | Director: {s.Director} | Ranking: {s.Ranking}");
                    foreach (var t in s.Temporadas)
                    {
                        sb.AppendLine($"      Temporada {t.Numero} - Episodios: {t.Episodios.Count}");
                        foreach (var e in t.Episodios)
                        {
                            sb.AppendLine($"        • {e.Nombre} ({e.DuracionMinutos} min)");
                        }
                    }
                }
            }
            return sb.ToString();
        }

        public string DescribirClienteConImporte(string codigoCliente)
        {
            var cl = rClientes.BuscarPorCodigo(codigoCliente);
            if (cl == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            string paqueteStr = string.IsNullOrWhiteSpace(cl.PaqueteCodigo) ? "(sin paquete)" : cl.PaqueteCodigo;
            decimal importe = CalcularImporteMensualCliente(codigoCliente);
            return $"{cl.CodigoCliente} - {cl.Nombre} {cl.Apellido} - DNI {cl.Dni} - Paquete: {paqueteStr} - Importe mensual: ${importe:N2}";
        }

        
        private Paquete BuscarPaqueteObligatorio(string codigoPaquete)
        {
            var p = rPaquetes.BuscarPorCodigo(codigoPaquete);
            if (p == null) throw new EntidadNoEncontradaException("Paquete no encontrado.");
            return p;
        }

        private Canal BuscarCanal(Paquete p, string codigoCanal)
        {
            foreach (var c in p.Canales) if (c.Codigo == codigoCanal) return c; return null;
        }

        private Canal BuscarCanalObligatorio(string codigoPaquete, string codigoCanal)
        {
            var p = BuscarPaqueteObligatorio(codigoPaquete);
            var c = BuscarCanal(p, codigoCanal);
            if (c == null) throw new EntidadNoEncontradaException("Canal no encontrado en el paquete.");
            return c;
        }

        private Serie BuscarSerie(Canal canal, string nombreSerie)
        {
            foreach (var s in canal.Series) if (string.Equals(s.Nombre, nombreSerie, StringComparison.OrdinalIgnoreCase)) return s; return null;
        }

        private Serie BuscarSerieObligatoria(string codigoPaquete, string codigoCanal, string nombreSerie)
        {
            var canal = BuscarCanalObligatorio(codigoPaquete, codigoCanal);
            var s = BuscarSerie(canal, nombreSerie);
            if (s == null) throw new EntidadNoEncontradaException("Serie no encontrada en el canal.");
            return s;
        }

        private Temporada BuscarTemporada(Serie serie, int numero)
        {
            foreach (var t in serie.Temporadas) if (t.Numero == numero) return t; return null;
        }

        private Temporada BuscarTemporadaObligatoria(string codigoPaquete, string codigoCanal, string nombreSerie, int numero)
        {
            var s = BuscarSerieObligatoria(codigoPaquete, codigoCanal, nombreSerie);
            var t = BuscarTemporada(s, numero);
            if (t == null) throw new EntidadNoEncontradaException("Temporada no encontrada en la serie.");
            return t;
        }

        private Episodio BuscarEpisodio(Temporada temp, string nombreEpisodio)
        {
            foreach (var e in temp.Episodios) if (string.Equals(e.Nombre, nombreEpisodio, StringComparison.OrdinalIgnoreCase)) return e; return null;
        }


    }
}
