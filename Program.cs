using System;
using System.Collections.Generic;

namespace TP03_InfoPalooza_Rasumoff_Sfintzi
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MIN_MENU = 1, MAX_MENU = 5;
            const int PRECIO0 = 45000, PRECIO1 = 60000, PRECIO2 = 30000, PRECIO3 = 100000;
            int opcion = 0;

            do
            {
                Console.WriteLine("============================");
                Console.WriteLine("|¡Bienvenido a InfoPalooza!|");
                Console.WriteLine("============================");
                Console.WriteLine();
                Console.WriteLine("Elija una de las siguientes funciones:");
                Console.WriteLine("1: Nueva Inscripción");
                Console.WriteLine("2: Obtener Estadísticas del Evento");
                Console.WriteLine("3: Buscar Cliente");
                Console.WriteLine("4: Cambiar entrada de un Cliente");
                Console.WriteLine("5: Salir");

                opcion = int.Parse(Console.ReadLine());
                opcion = ValidarNum(opcion, MIN_MENU, MAX_MENU);

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese su DNI:");
                        int dni = PasarInt(Console.ReadLine(), "ERROR. Ingrese el DNI de la persona:");
                        Console.WriteLine("Ingrese su nombre:");
                        string nombre = ValidarString(Console.ReadLine(), "ERROR. Ingrese su nombre:");
                        Console.WriteLine("Ingrese su apellido:");
                        string apellido = ValidarString(Console.ReadLine(), "ERROR. Ingrese su apellido:");
                        Console.WriteLine("Ingrese el tipo de entrada que quiere comprar:");
                        Console.WriteLine("1 - Día 1 , valor a abonar $45000");
                        Console.WriteLine("2 - Día 2 , valor a abonar $60000");
                        Console.WriteLine("3 - Día 3 , valor a abonar $30000");
                        Console.WriteLine("4 - Full Pass , valor a abonar $100000");

                        int tipoEntrada = ValidarNum(int.Parse(Console.ReadLine()), 1, 4);
                        Console.WriteLine("Ingrese la cantidad de entradas que quiere comprar:");
                        int cantidad = PasarInt(Console.ReadLine(), "ERROR. Ingrese la cantidad de entradas que quiere comprar:");
                        
                        int idCliente = Tiquetera.AgregarCliente(dni, apellido, nombre, tipoEntrada, cantidad);
                        Console.WriteLine($"Cliente registrado con ID: {idCliente}");
                        break;

                    case 2:
                        List<Cliente> clientes = Tiquetera.ObtenerClientes();
                        int totalClientes = clientes.Count;
                        int cantidadEntradas0 = 0, cantidadEntradas1 = 0, cantidadEntradas2 = 0, cantidadEntradas3 = 0;
                        int recaudacionEntradas0 = 0, recaudacionEntradas1 = 0, recaudacionEntradas2 = 0, recaudacionEntradas3 = 0;
                        int recaudacionTotal = 0;

                        foreach (Cliente cliente in clientes)
                        {
                            switch (cliente.TipoEntrada)
                            {
                                case 0:
                                    cantidadEntradas0 += cliente.Cantidad;
                                    recaudacionEntradas0 += cliente.Cantidad * PRECIO0;
                                    break;
                                case 1:
                                    cantidadEntradas1 += cliente.Cantidad;
                                    recaudacionEntradas1 += cliente.Cantidad * PRECIO1;
                                    break;
                                case 2:
                                    cantidadEntradas2 += cliente.Cantidad;
                                    recaudacionEntradas2 += cliente.Cantidad * PRECIO2;
                                    break;
                                case 3:
                                    cantidadEntradas3 += cliente.Cantidad;
                                    recaudacionEntradas3 += cliente.Cantidad * PRECIO3;
                                    break;
                                default:
                                    break;
                            }
                            recaudacionTotal += cliente.Cantidad * Tiquetera.ObtenerPrecio(cliente.TipoEntrada, PRECIO0, PRECIO1, PRECIO2, PRECIO3);
                        }

                        List<string> estadisticas = Tiquetera.EstadisticasTiquetera(totalClientes, cantidadEntradas0, cantidadEntradas1, cantidadEntradas2, cantidadEntradas3, recaudacionEntradas0, recaudacionEntradas1, recaudacionEntradas2, recaudacionEntradas3, recaudacionTotal);

                        Console.WriteLine();
                        Console.WriteLine("---------------ESTADÍSTICAS DE LA TIQUETERA---------------");
                        Console.WriteLine();
                        foreach (string estadistica in estadisticas)
                        {
                            Console.WriteLine(estadistica);
                        }
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine("Ingrese el ID del TICKET del cliente que desea buscar:");
                        int id = PasarInt(Console.ReadLine(), "ERROR. Ingrese el ID del TICKET (solo numérico):");
                        Cliente clienteY = Tiquetera.BuscarCliente(id);
                        if (clienteY == null)
                        {
                            Console.WriteLine("Lo sentimos, no hemos encontrado un tiquet con ese ID");
                        }
                        else
                        {
                        Console.WriteLine($"Nombre y Apellido del cliente: {clienteY.Nombre} {clienteY.Apellido}\nDNI: {clienteY.DNI}\nFecha de Inscripción: {clienteY.FechaInscripcion}\nCantidad de entradas compradas: {clienteY.Cantidad}\nTipo de entradas compradas: {clienteY.TipoEntrada}");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Ingrese el ID del TICKET del cliente que desea cambiar la entrada:");
                        int idCambiar = PasarInt(Console.ReadLine(), "ERROR. Ingrese el ID del TICKET (solo numérico):");
                        Console.WriteLine("Ingrese el nuevo tipo de entrada (1, 2, 3 ó 4):");
                        int tipoEntradaNueva = ValidarNum(int.Parse(Console.ReadLine()), 1, 4);
                        Console.WriteLine("Ingrese la nueva cantidad de entradas:");
                        int cantidadNueva = PasarInt(Console.ReadLine(), "ERROR. Ingrese la nueva cantidad de entradas:");

                        bool cambioExitoso = Tiquetera.CambiarEntrada(idCambiar, tipoEntradaNueva, cantidadNueva, PRECIO0, PRECIO1, PRECIO2, PRECIO3);
                        if (cambioExitoso)
                        {
                            Console.WriteLine("Cambio de entrada realizado con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo realizar el cambio de entrada.");
                        }
                        break;

                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine();

            } while (opcion != MAX_MENU);
        }

        static int ValidarNum(int num, int min, int max)
        {
            while (num < min || num > max)
            {
                Console.WriteLine($"Número ingresado incorrecto. Ingrese un número entre {min} y {max}:");
                num = int.Parse(Console.ReadLine());
            }

            return num;
        }

        static string ValidarString(string frase, string msj)
        {
            while (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine(msj);
                frase = Console.ReadLine();
            }

            return frase;
        }

        static int PasarInt(string frase, string msj)
        {
            int num;
            while (!int.TryParse(frase, out num))
            {
                Console.WriteLine(msj);
                frase = Console.ReadLine();
            }

            return num;
        }
    }
}


