using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Negocio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreC { get; set; }
        public string ApellidoC { get; set; }
        public string CalleC { get; set; }
        public string NumeroC { get; set; }
        public string TelefonoC { get; set; }

        public bool Crear()
        {
            try
            {
                Datos.Cliente cliente = new Datos.Cliente()
                {
                    nombre_cliente = NombreC,
                    apellido_cliente = ApellidoC,
                    calle_cliente = CalleC,
                    numero_calle = NumeroC,
                    telefono_cliente = TelefonoC,
                    id_cliente = IdCliente,
                };
                Connection.DBTienda.Cliente.Add(cliente);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Buscar(string telefono)
        {
            try
            {
                Datos.Cliente cliente = Connection.DBTienda.Cliente.First(c => c.telefono_cliente == telefono);
                IdCliente = cliente.id_cliente;
                NombreC = cliente.nombre_cliente;
                ApellidoC = cliente.apellido_cliente;
                CalleC = cliente.calle_cliente;
                NumeroC = cliente.numero_calle;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
    