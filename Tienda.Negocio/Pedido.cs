using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Negocio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string nombreC { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public int IdCarro { get; set; }
        public int CodigoCarro { get; set; }
        public int TotalPrecio { get; set; }

        public bool Crear()
        {
            try
            {
                Datos.Pedido pedido = new Datos.Pedido()
                {
                    id_pedido = IdPedido,
                    estado = Estado,
                    fecha = Fecha,
                    hora = Hora,
                    id_carro_compra = IdCarro,
                    codigo_carro = CodigoCarro,
                    total_precio = TotalPrecio,
                };
                Connection.DBTienda.Pedido.Add(pedido);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Pedido> Listar()
        {
            List<Pedido> listaPedido = new List<Pedido>();
            foreach (Datos.Pedido item in Connection.DBTienda.Pedido.ToList())
            {
                listaPedido.Add(new Pedido()
                {
                    IdPedido = item.id_pedido,
                    Estado = item.estado,
                    Fecha = item.fecha,
                    Hora = item.hora,
                    IdCarro = item.id_carro_compra,
                    CodigoCarro = item.codigo_carro,
                    TotalPrecio = item.total_precio,
                    nombreC = item.Carro_Compra.Cliente.nombre_cliente,
                });
            }
            return listaPedido;
        }

        public bool Buscar(int codigoC)
        {
            try
            {
                Datos.Pedido pedido = Connection.DBTienda.Pedido.First(p => p.codigo_carro == codigoC);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool BuscarPedido()
        {
            try
            {
                Datos.Pedido pedido = Connection.DBTienda.Pedido.First(p => p.id_pedido == IdPedido);
                IdPedido = pedido.id_pedido;
                nombreC = pedido.Carro_Compra.Cliente.nombre_cliente;
                Estado = pedido.estado;
                Fecha = pedido.fecha;
                Hora = pedido.hora;
                IdCarro = pedido.id_carro_compra;
                CodigoCarro = pedido.codigo_carro;
                TotalPrecio = pedido.total_precio;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar()
        {
            try
            {
                Datos.Pedido pedidoActualizado = Connection.DBTienda.Pedido.First(ped => ped.id_pedido == IdPedido);
                pedidoActualizado.estado = Estado;
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
