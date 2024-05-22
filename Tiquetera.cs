using System;
using System.Collections.Generic;

static class Tiquetera
{
    private static Dictionary<int, Cliente> DicClientes = new Dictionary<int, Cliente>();
    private static int UltimoIDEntrada = 0;

    public static int DevolverUltimoId()
    {
        return UltimoIDEntrada;
    }

    public static int AgregarCliente(int dni, string apellido, string nombre, int tipoEntrada, int cantidad)
    {
        int id = UltimoIDEntrada++;
        Cliente nuevoCliente = new Cliente(dni, apellido, nombre, DateTime.Now, tipoEntrada, cantidad);
        DicClientes.Add(id, nuevoCliente);
        return id;
    }

    public static Cliente BuscarCliente(int id)
    {
        if (DicClientes.ContainsKey(id))
        {
            return DicClientes[id];
        }
        else
        {
            return null;
        }
    }
public static bool CambiarEntrada(int id, int tipoEntradaNueva, int cantidadNueva, int precio0, int precio1, int precio2, int precio3)
{
    if (DicClientes.ContainsKey(id))
    {
        Cliente cliente = DicClientes[id];
        int importeActual = cliente.Cantidad * ObtenerPrecio(cliente.TipoEntrada, precio0, precio1, precio2, precio3);
        int importeNuevo = cantidadNueva * ObtenerPrecio(tipoEntradaNueva, precio0, precio1, precio2, precio3);

        if (importeNuevo > importeActual)
        {
            cliente.Cantidad = cantidadNueva;
            cliente.TipoEntrada = tipoEntradaNueva;
            return true;
        }
    }
    return false;
}

public static int ObtenerPrecio(int tipoEntrada, int precio0, int precio1, int precio2, int precio3)
{
    switch (tipoEntrada)
    {
        case 0:
            return precio0;
        case 1:
            return precio1;
        case 2:
            return precio2;
        case 3:
            return precio3;
        default:
            return 0;
    }
}


public static List<string> EstadisticasTiquetera(int cantidadClientes, int cantidadEntradas0, int cantidadEntradas1, int cantidadEntradas2, int cantidadEntradas3, int recaudacionEntradas0, int recaudacionEntradas1, int recaudacionEntradas2, int recaudacionEntradas3, int recaudacionTotal)
{
    List<string> estadisticas = new List<string>();

    if (cantidadClientes > 0)
    {
        double porcentajeEntradas0 = (double)cantidadEntradas0 / cantidadClientes * 100;
        double porcentajeEntradas1 = (double)cantidadEntradas1 / cantidadClientes * 100;
        double porcentajeEntradas2 = (double)cantidadEntradas2 / cantidadClientes * 100;
        double porcentajeEntradas3 = (double)cantidadEntradas3 / cantidadClientes * 100;

        estadisticas.Add($"Cantidad Total de clientes inscriptos: {cantidadClientes}");
        estadisticas.Add($"Entrada tipo 1:\n>Cantidad de clientes: {cantidadEntradas0} --- {porcentajeEntradas0}% del total\n>Recaudación: {recaudacionEntradas0}");
        estadisticas.Add($"Entrada tipo 2:\n>Cantidad de clientes: {cantidadEntradas1} --- {porcentajeEntradas1}% del total\n>Recaudación: {recaudacionEntradas1}");
        estadisticas.Add($"Entrada tipo 3:\n>Cantidad de clientes: {cantidadEntradas2} --- {porcentajeEntradas2}% del total\n>Recaudación: {recaudacionEntradas2}");
        estadisticas.Add($"Entrada tipo 4:\n>Cantidad de clientes: {cantidadEntradas3} --- {porcentajeEntradas3}% del total\n>Recaudación: {recaudacionEntradas3}");
        estadisticas.Add($"Recaudación total: {recaudacionTotal}");
    }
    else
    {
        estadisticas.Add("Aún no se anotó nadie");
    }

    return estadisticas;
}


public static List<Cliente> ObtenerClientes()
{
    return DicClientes.Values.ToList();
}

}
