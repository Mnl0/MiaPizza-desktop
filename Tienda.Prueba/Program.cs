using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Datos;

namespace Tienda.Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            Categoria categoria = new Categoria()
            {
                id_categoria = 1,
                nombre_categoria = "prueba desde la consola",
                descripcion_categoria = "descripcion desde la consola",
            };
            Console.ReadKey();
        }
    }
}

