using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Negocio
{
    public class Carro_Compra
    {
        public int IdCarroCompra { get; set; }
        public int? IdCliente { get; set; }
        public string NombreC { get; set; }
        public int? IdProducto { get; set; }
        public string NombreP { get; set; }
        public int? ValorUnitario { get; set; }
        public int? Cantidad { get; set; }
        public int? CodigoCarro { get; set; }
        public int? SubTotal { get; set; }

        public bool Crear()
        {
            try
            {
                Datos.Carro_Compra carroCompra = new Datos.Carro_Compra()
                {
                    id_cliente = IdCliente,
                    id_producto = IdProducto,
                    cantidad = Cantidad,
                    codigo_carro = CodigoCarro,
                    valor_unitario = ValorUnitario,
                    sub_total = ValorUnitario * Cantidad,
                };
                Connection.DBTienda.Carro_Compra.Add(carroCompra);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Buscar(int? codigo)
        {
            try
            {
                Datos.Carro_Compra carroBuscado = Connection.DBTienda.Carro_Compra.First(c => c.codigo_carro == codigo);
                IdCarroCompra = carroBuscado.id_carro_compra;
                ValorUnitario = carroBuscado.valor_unitario;
                SubTotal = carroBuscado.sub_total;
                Cantidad = carroBuscado.cantidad;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Carro_Compra> ListarCodigoCarro(int? carro)
        {
            List<Carro_Compra> listaT = new List<Carro_Compra>();
            foreach (Datos.Carro_Compra item in Connection.DBTienda.Carro_Compra.Where(c => c.codigo_carro == carro).ToList())
            {
                listaT.Add(new Carro_Compra()
                {
                    IdCarroCompra = item.id_carro_compra,
                    IdCliente = item.id_cliente,
                    NombreC = item.Cliente.nombre_cliente,
                    IdProducto = item.id_producto,
                    NombreP = item.Producto.nombre_producto,
                    ValorUnitario = item.Producto.precio_producto,
                    Cantidad = item.cantidad,
                    CodigoCarro = item.codigo_carro,
                    SubTotal = item.valor_unitario * item.cantidad,
                });
            }
            return listaT;
        }

    }
}
