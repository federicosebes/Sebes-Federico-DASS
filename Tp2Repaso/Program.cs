// See https://aka.ms/new-console-template for more information


namespace Tp2Repaso 
{
    public class DatosInvalidosException : Exception { public DatosInvalidosException(string m) : base(m) { } }
    public class DniDuplicadoException : Exception { public DniDuplicadoException(string m) : base(m) { } }
    public class CodigoDuplicadoException : Exception { public CodigoDuplicadoException(string m) : base(m) { } }
    public class EntidadNoEncontradaException : Exception { public EntidadNoEncontradaException(string m) : base(m) { } }
    internal class Program 
    {
        private static CentroServicio central = new CentroServicio();
        static void Main(string[] args) 
        {
            bool seguir = true;
            while (seguir)
            {
                try
                {
                    MostrarMenu();
                    int op = LeerInt("Opción: ");
                    Console.WriteLine();
                    switch (op)
                    {
                        case 1: AltaCliente(); break;
                        case 2: ModCliente(); break;
                        case 3: BajaCliente(); break;
                        case 4: AltaPaquete(); break;
                        case 5: AgregarCanal(); break;
                        case 6: AgregarSerie(); break;
                        case 7: AgregarTemporada(); break;
                        case 8: AgregarEpisodio(); break;
                        case 9: ContratarPaquete(); break;
                        case 10: ListarPaquetes(); break;
                        case 11: ListarClientesConImporte(); break;
                        case 12: TotalRecaudado(); break;
                        case 13: PaqueteMasVendido(); break;
                        case 14: SeriesRanking(); break;
                        case 0: seguir = false; break;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }

                if (seguir)
                {
                    Console.WriteLine();
                    Console.Write("Presione una tecla...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== EMPRESA DE CABLE ===");
            Console.WriteLine("1. Alta cliente");
            Console.WriteLine("2. Modificar cliente");
            Console.WriteLine("3. Eliminar cliente (sin paquete)");
            Console.WriteLine("4. Crear paquete (Basico/Silver/Premium)");
            Console.WriteLine("5. Agregar canal a paquete");
            Console.WriteLine("6. Agregar serie a canal");
            Console.WriteLine("7. Agregar temporada a serie");
            Console.WriteLine("8. Agregar episodio a temporada");
            Console.WriteLine("9. Contratar paquete para cliente");
            Console.WriteLine("10. Informar paquetes (estructura completa)");
            Console.WriteLine("11. Informar clientes con importes");
            Console.WriteLine("12. Total recaudado mensual");
            Console.WriteLine("13. Paquete más vendido (y mostrar sus series)");
            Console.WriteLine("14. Listar series con ranking > 3.5");
            Console.WriteLine("0. Salir");
            Console.WriteLine("------------------------");
        }

        
        static void AltaCliente()
        {
            Console.WriteLine("ALTA CLIENTE");
            string cod = LeerTxt("Código de cliente: ");
            string nom = LeerTxt("Nombre: ");
            string ape = LeerTxt("Apellido: ");
            string dni = LeerTxt("DNI: ");
            DateTime fn = LeerFecha("Fecha de nacimiento (dd/mm/aaaa): ");
            decimal abono = LeerDec("Abono base: ");
            central.CrearCliente(cod, nom, ape, dni, fn, abono);
            Console.WriteLine("Cliente creado.");
        }

        static void ModCliente()
        {
            Console.WriteLine("MODIFICAR CLIENTE");
            string cod = LeerTxt("Código (existente): ");
            var ex = central.BuscarClientePorCodigo(cod);
            if (ex == null) throw new EntidadNoEncontradaException("Cliente no encontrado.");
            string nom = LeerTxt($"Nombre ({ex.Nombre}): ");
            string ape = LeerTxt($"Apellido ({ex.Apellido}): ");
            string dni = LeerTxt($"DNI ({ex.Dni}): ");
            DateTime fn = LeerFecha("Fecha de nacimiento (dd/mm/aaaa): ");
            decimal abono = LeerDec("Abono base: ");
            Cliente c = new Cliente();
            c.CodigoCliente = cod; c.Nombre = nom; c.Apellido = ape; c.Dni = dni; c.FechaNacimiento = fn; c.AbonoBase = abono; c.PaqueteCodigo = ex.PaqueteCodigo;
            central.ModificarCliente(c);
            Console.WriteLine("Cliente modificado.");
        }

        static void BajaCliente()
        {
            Console.WriteLine("ELIMINAR CLIENTE");
            string cod = LeerTxt("Código: ");
            central.EliminarCliente(cod);
            Console.WriteLine("Cliente eliminado.");
        }

        
        static void AltaPaquete()
        {
            Console.WriteLine("ALTA PAQUETE");
            string cod = LeerTxt("Código de paquete: ");
            string nom = LeerTxt("Nombre de paquete: ");
            string tipo = LeerTxt("Tipo (Basico/Silver/Premium): ");
            central.CrearPaquete(cod, nom, tipo);
            Console.WriteLine("Paquete creado.");
        }

        static void AgregarCanal()
        {
            Console.WriteLine("AGREGAR CANAL A PAQUETE");
            string codP = LeerTxt("Código de paquete: ");
            string codC = LeerTxt("Código de canal: ");
            string nomC = LeerTxt("Nombre de canal: ");
            central.AgregarCanalAPaquete(codP, codC, nomC);
            Console.WriteLine("Canal agregado.");
        }

        static void AgregarSerie()
        {
            Console.WriteLine("AGREGAR SERIE A CANAL");
            string codP = LeerTxt("Código de paquete: ");
            string codC = LeerTxt("Código de canal: ");
            string nomS = LeerTxt("Nombre de la serie: ");
            string genero = LeerTxt("Género: ");
            string director = LeerTxt("Director: ");
            decimal ranking = LeerDec("Ranking (ej 4.2): ");
            central.AgregarSerieACanal(codP, codC, nomS, genero, director, ranking);
            Console.WriteLine("Serie agregada.");
        }

        static void AgregarTemporada()
        {
            Console.WriteLine("AGREGAR TEMPORADA A SERIE");
            string codP = LeerTxt("Código de paquete: ");
            string codC = LeerTxt("Código de canal: ");
            string nomS = LeerTxt("Nombre de la serie: ");
            int num = LeerInt("N° de temporada: ");
            central.AgregarTemporadaASerie(codP, codC, nomS, num);
            Console.WriteLine("Temporada agregada.");
        }

        static void AgregarEpisodio()
        {
            Console.WriteLine("AGREGAR EPISODIO A TEMPORADA");
            string codP = LeerTxt("Código de paquete: ");
            string codC = LeerTxt("Código de canal: ");
            string nomS = LeerTxt("Nombre de la serie: ");
            int num = LeerInt("N° de temporada: ");
            string nomE = LeerTxt("Nombre del episodio: ");
            int dur = LeerInt("Duración (min): ");
            central.AgregarEpisodioATemporada(codP, codC, nomS, num, nomE, dur);
            Console.WriteLine("Episodio agregado.");
        }

        
        static void ContratarPaquete()
        {
            Console.WriteLine("CONTRATAR PAQUETE PARA CLIENTE");
            string codCl = LeerTxt("Código de cliente: ");
            string codP = LeerTxt("Código de paquete: ");
            central.ContratarPaquete(codCl, codP);
            Console.WriteLine("Paquete contratado.");
        }

        
        static void ListarPaquetes()
        {
            Console.WriteLine("PAQUETES Y CONTENIDO");
            var ps = central.ListarPaquetes();
            if (ps.Count == 0) { Console.WriteLine("No hay paquetes."); return; }
            foreach (var p in ps)
            {
                Console.WriteLine(central.DescribirPaquete(p.Codigo));
            }
        }

        static void ListarClientesConImporte()
        {
            Console.WriteLine("CLIENTES E IMPORTES");
            var cls = central.ListarClientes();
            if (cls.Count == 0) { Console.WriteLine("No hay clientes."); return; }
            foreach (var c in cls)
            {
                Console.WriteLine(central.DescribirClienteConImporte(c.CodigoCliente));
            }
        }

        static void TotalRecaudado()
        {
            decimal t = central.TotalRecaudadoMensual();
            Console.WriteLine($"Total recaudado mensual: ${t:N2}");
        }

        static void PaqueteMasVendido()
        {
            var p = central.PaqueteMasVendido();
            if (p == null) { Console.WriteLine("No hay ventas de paquetes aún."); return; }
            Console.WriteLine($"PAQUETE MÁS VENDIDO: {p.Codigo} - {p.Nombre} ({p.Tipo})");
            Console.WriteLine("Series incluidas:");
            foreach (var canal in p.Canales)
            {
                foreach (var s in canal.Series)
                {
                    Console.WriteLine($"  - {s.Nombre} (Ranking {s.Ranking})");
                }
            }
        }

        static void SeriesRanking()
        {
            Console.WriteLine("SERIES CON RANKING > 3.5");
            var lista = central.SeriesConRankingMayorA(3.5m);
            if (lista.Count == 0) { Console.WriteLine("Sin resultados."); return; }
            foreach (var s in lista)
            {
                Console.WriteLine($"- {s.Nombre} | {s.Genero} | Dir: {s.Director} | Ranking: {s.Ranking}");
            }
        }

        
        static int LeerInt(string msg)
        {
            Console.Write(msg); int v; while (!int.TryParse(Console.ReadLine(), out v)) { Console.Write("Inválido: "); }
            return v;
        }
        static decimal LeerDec(string msg)
        {
            Console.Write(msg); decimal v; while (!decimal.TryParse(Console.ReadLine(), out v) || v < 0) { Console.Write("Inválido: "); }
            return v;
        }
        static string LeerTxt(string msg)
        {
            Console.Write(msg); string t = Console.ReadLine(); while (string.IsNullOrWhiteSpace(t)) { Console.Write("Obligatorio: "); t = Console.ReadLine(); }
            return t.Trim();
        }
        static DateTime LeerFecha(string msg)
        {
            Console.Write(msg); DateTime f; while (!DateTime.TryParse(Console.ReadLine(), out f)) { Console.Write("Fecha inválida: "); }
            return f.Date;
        }

    
    }
}
