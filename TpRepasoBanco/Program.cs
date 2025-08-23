// See https://aka.ms/new-console-template for more information

namespace TpRepasoBanco
{
    public class FondosInsuficientesException : Exception
    {
        public FondosInsuficientesException(string mensaje) : base(mensaje) { }
    }

    public class LimiteRetirosExcedidoException : Exception
    {
        public LimiteRetirosExcedidoException(string mensaje) : base(mensaje) { }
    }

    public class DatosInvalidosException : Exception
    {
        public DatosInvalidosException(string mensaje) : base(mensaje) { }
    }

    public class DniDuplicadoException : Exception
    {
        public DniDuplicadoException(string mensaje) : base(mensaje) { }
    }

    public class CuentaDuplicadaException : Exception
    {
        public CuentaDuplicadaException(string mensaje) : base(mensaje) { }
    }







    internal class Program
    {
        private static BancoCentral banco = new BancoCentral();

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                try
                {
                    MostrarMenu();
                    int opcion = LeerEntero("Opción: ");
                    Console.WriteLine();

                    switch (opcion)
                    {
                        case 1: AltaCliente(); break;
                        case 2: ModificarCliente(); break;
                        case 3: EliminarCliente(); break;
                        case 4: CrearCuenta(); break;
                        case 5: EliminarCuenta(); break;
                        case 6: CambiarTitularCuenta(); break;
                        case 7: Depositar(); break;
                        case 8: Retirar(); break;
                        case 9: ConsultarSaldo(); break;
                        case 10: ListarClientes(); break;
                        case 11: BuscarCliente(); break;
                        case 0: continuar = false; break;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }

                if (continuar)
                {
                    Console.WriteLine();
                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== BANCO - Gestión de Clientes y Cuentas ===");
            Console.WriteLine("1. Agregar cliente");
            Console.WriteLine("2. Modificar cliente");
            Console.WriteLine("3. Eliminar cliente");
            Console.WriteLine("4. Crear cuenta");
            Console.WriteLine("5. Eliminar cuenta");
            Console.WriteLine("6. Cambiar titular de cuenta");
            Console.WriteLine("7. Depósito");
            Console.WriteLine("8. Extracción");
            Console.WriteLine("9. Consultar saldo");
            Console.WriteLine("10. Listar clientes");
            Console.WriteLine("11. Buscar cliente (por DNI o nombre)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("---------------------------------------------");
        }

        // ====== CLIENTES ======
        static void AltaCliente()
        {
            Console.WriteLine("ALTA CLIENTE");
            string dni = LeerTextoObligatorio("DNI: ");
            string nom = LeerTextoObligatorio("Nombre y apellido: ");
            string tel = LeerTextoObligatorio("Teléfono: ");
            string mail = LeerTextoObligatorio("Email: ");
            DateTime fnac = LeerFecha("Fecha de nacimiento (dd/mm/aaaa): ");

            banco.CrearCliente(dni, nom, tel, mail, fnac);
            Console.WriteLine("Cliente agregado correctamente.");
        }

        static void ModificarCliente()
        {
            Console.WriteLine("MODIFICAR CLIENTE");
            string dni = LeerTextoObligatorio("DNI (existente): ");
            var cli = banco.BuscarCliente(dni);
            if (cli == null) throw new DatosInvalidosException("Cliente no encontrado.");

            string nom = LeerTextoObligatorio($"Nombre y apellido ({cli.Titular}): ");
            string tel = LeerTextoObligatorio($"Teléfono ({cli.Ntelefono}): ");
            string mail = LeerTextoObligatorio($"Email ({cli.Mail}): ");
            DateTime fnac = LeerFecha("Fecha de nacimiento (dd/mm/aaaa): ");

            var actualizado = new Cliente
            {
                Dni = dni,
                Titular = nom,
                Ntelefono = tel,
                Mail = mail,
                Fechanacimiento = fnac
            };

            banco.ModificarCliente(actualizado);
            Console.WriteLine("Cliente modificado.");
        }

        static void EliminarCliente()
        {
            Console.WriteLine("ELIMINAR CLIENTE");
            string dni = LeerTextoObligatorio("DNI: ");
            banco.EliminarCliente(dni);
            Console.WriteLine("Cliente eliminado.");
        }

        // ====== CUENTAS ======
        static void CrearCuenta()
        {
            Console.WriteLine("CREAR CUENTA");
            string numero = LeerTextoObligatorio("Número de cuenta (único): ");
            string dniTitular = LeerTextoObligatorio("DNI del titular (debe existir): ");

            Console.WriteLine("Tipo de cuenta: 1) Caja de Ahorros  2) Cuenta Corriente");
            int tipo = LeerEntero("Tipo: ");

            if (tipo == 1)
            {
                banco.CrearCuentaAhorros(numero, dniTitular);
                Console.WriteLine("Caja de Ahorros creada.");
            }
            else if (tipo == 2)
            {
                banco.CrearCuentaCorriente(numero, dniTitular);
                Console.WriteLine("Cuenta Corriente creada.");
            }
            else
            {
                Console.WriteLine("Tipo inválido.");
            }
        }

        static void EliminarCuenta()
        {
            Console.WriteLine("ELIMINAR CUENTA");
            string numero = LeerTextoObligatorio("Número de cuenta: ");
            banco.EliminarCuenta(numero);
            Console.WriteLine("Cuenta eliminada.");
        }

        static void CambiarTitularCuenta()
        {
            Console.WriteLine("CAMBIAR TITULAR DE CUENTA");
            string numero = LeerTextoObligatorio("Número de cuenta: ");
            string nuevoDni = LeerTextoObligatorio("Nuevo DNI de titular (debe existir): ");
            banco.CambiarTitularCuenta(numero, nuevoDni);
            Console.WriteLine("Titular cambiado.");
        }

      
        static void Depositar()
        {
            Console.WriteLine("DEPÓSITO");
            string numero = LeerTextoObligatorio("Número de cuenta: ");
            decimal importe = LeerDecimalPositivo("Importe a depositar: ");
            banco.RealizarDeposito(numero, importe);
            Console.WriteLine("Depósito realizado.");
        }

        static void Retirar()
        {
            Console.WriteLine("EXTRACCIÓN");
            string numero = LeerTextoObligatorio("Número de cuenta: ");
            decimal importe = LeerDecimalPositivo("Importe a extraer: ");
            banco.RealizarRetiro(numero, importe);
            Console.WriteLine("Extracción realizada.");
        }

        static void ConsultarSaldo()
        {
            Console.WriteLine("CONSULTA DE SALDO");
            string numero = LeerTextoObligatorio("Número de cuenta: ");
            decimal s = banco.ConsultarSaldo(numero);
            Console.WriteLine($"Saldo de la cuenta {numero}: ${s:N2}");
        }

        // ====== LISTADOS / BÚSQUEDA ======
        static void ListarClientes()
        {
            Console.WriteLine("LISTADO DE CLIENTES");
            var lista = banco.ObtenerTodosLosClientes();
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay clientes cargados.");
                return;
            }

            var cuentas = banco.ObtenerTodasLasCuentas();

            foreach (var c in lista)
            {
                Console.WriteLine($"{c.Dni} - {c.Titular} - Tel: {c.Ntelefono} - Email: {c.Mail}");
                var ctasCli = cuentas.Where(cta => cta.DniTitular == c.Dni).ToList();
                if (ctasCli.Count == 0)
                {
                    Console.WriteLine("  (Sin cuentas)");
                }
                else
                {
                    foreach (var cu in ctasCli)
                    {
                        Console.WriteLine($"  Cuenta {cu.Ncuenta} ({cu.ObtenerTipoCuenta()}) - Saldo: ${cu.saldo:N2}");
                    }
                }
            }
        }

        static void BuscarCliente()
        {
            Console.WriteLine("BUSCAR CLIENTE");
            Console.WriteLine("1) Por DNI  2) Por nombre");
            int tipo = LeerEntero("Tipo: ");

            if (tipo == 1)
            {
                string dni = LeerTextoObligatorio("DNI: ");
                var c = banco.BuscarCliente(dni);
                if (c == null) Console.WriteLine("No se encontró el cliente.");
                else Console.WriteLine($"{c.Dni} - {c.Titular} - Tel: {c.Ntelefono} - Email: {c.Mail}");
            }
            else if (tipo == 2)
            {
                string nombre = LeerTextoObligatorio("Nombre (o parte): ");
                var todos = banco.ObtenerTodosLosClientes();
                var lista = todos.Where(x => !string.IsNullOrWhiteSpace(x.Titular) &&
                                             x.Titular.ToLower().Contains(nombre.ToLower()))
                                 .ToList();
                if (lista.Count == 0) Console.WriteLine("Sin coincidencias.");
                else foreach (var c in lista) Console.WriteLine($"{c.Dni} - {c.Titular}");
            }
            else
            {
                Console.WriteLine("Tipo inválido.");
            }
        }

        
        static int LeerEntero(string mensaje)
        {
            Console.Write(mensaje);
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Intente nuevamente: ");
            }
            return valor;
        }

        static decimal LeerDecimalPositivo(string mensaje)
        {
            Console.Write(mensaje);
            decimal valor;
            while (!decimal.TryParse(Console.ReadLine(), out valor) || valor < 0)
            {
                Console.Write("Valor inválido. Intente nuevamente: ");
            }
            return valor;
        }

        static string LeerTextoObligatorio(string mensaje)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(texto))
            {
                Console.Write("Este campo es obligatorio. Intente nuevamente: ");
                texto = Console.ReadLine();
            }
            return texto.Trim();
        }

        static DateTime LeerFecha(string mensaje)
        {
            Console.Write(mensaje);
            DateTime fecha;
            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.Write("Fecha inválida. Intente nuevamente: ");
            }
            return fecha.Date;
        }
    }
}
