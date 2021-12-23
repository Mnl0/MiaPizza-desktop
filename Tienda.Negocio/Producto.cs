using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Negocio
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreP { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int Precio { get; set; }
        public string DescripcionP { get; set; }

        public bool Crear()
        {
            try
            {
                Datos.Producto producto = new Datos.Producto()
                {
                    id_categoria = IdCategoria,
                    nombre_producto = NombreP,
                    descripcion_producto = DescripcionP,
                    precio_producto = Precio,
                };
                Connection.DBTienda.Producto.Add(producto);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Producto> Listar()
        {
            List<Producto> listaProducto = new List<Producto>();
            foreach (Datos.Producto item in Connection.DBTienda.Producto.ToList())
            {
                listaProducto.Add(new Producto()
                {
                    IdCategoria = item.id_categoria,
                    IdProducto = item.id_producto,
                    NombreP = item.nombre_producto,
                    DescripcionP = item.descripcion_producto,
                    Precio = item.precio_producto,
                });
            }
            return listaProducto;
        }

        public bool Buscar()
        {
            try
            {
                Datos.Producto productoBusc = Connection.DBTienda.Producto.First(prod => prod.id_producto == IdProducto);
                NombreP = productoBusc.nombre_producto;
                IdCategoria = productoBusc.id_categoria;
                Precio = productoBusc.precio_producto;
                DescripcionP = productoBusc.descripcion_producto;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actulizar()
        {
            try
            {
                Datos.Producto productoAct = Connection.DBTienda.Producto.First(prod => prod.id_producto == IdProducto);
                productoAct.id_categoria = IdCategoria;
                productoAct.nombre_producto = NombreP;
                productoAct.precio_producto = Precio;
                productoAct.descripcion_producto = DescripcionP;
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar()
        {
            try
            {
                Datos.Producto productoElim = Connection.DBTienda.Producto.First(prod => prod.id_producto == IdProducto);
                Connection.DBTienda.Producto.Remove(productoElim);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Producto> ListarP()
        {
            List<Producto> listaProducto = new List<Producto>();
            foreach (Datos.Producto item in Connection.DBTienda.Producto.ToList())
            {
                listaProducto.Add(new Producto()
                {
                    IdProducto = item.id_producto,
                    NombreP = item.nombre_producto,
                    IdCategoria = item.id_categoria,
                    NombreCategoria = item.Categoria.nombre_categoria,
                    DescripcionP = item.descripcion_producto,
                    Precio = item.precio_producto,
                });
            }
            return listaProducto;
        }

        public List<Producto> ListarDos(int idc)
        {
            List<Producto> listar = new List<Producto>();
            foreach (Datos.Producto item in Connection.DBTienda.Producto.Where(c => c.id_categoria == idc).ToList())
            {
                listar.Add(new Producto()
                {
                    IdCategoria = item.id_categoria,
                    IdProducto = item.id_producto,
                    NombreP = item.nombre_producto,
                    DescripcionP = item.descripcion_producto,
                    Precio = item.precio_producto,
                });
            }
            return listar;
        }
    }
}
