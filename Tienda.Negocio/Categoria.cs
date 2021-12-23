using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Negocio
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Crear()
        {
            try
            {
                Datos.Categoria categoria = new Datos.Categoria()
                {
                    id_categoria = IdCategoria,
                    nombre_categoria = Nombre,
                    descripcion_categoria = Descripcion
                };
                Connection.DBTienda.Categoria.Add(categoria);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Buscar()
        {
            try
            {
                Datos.Categoria categoriaBuscada = Connection.DBTienda.Categoria.First(cat => cat.id_categoria == IdCategoria);
                Nombre = categoriaBuscada.nombre_categoria;
                Descripcion = categoriaBuscada.descripcion_categoria;
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
                Datos.Categoria categoriaModificar = Connection.DBTienda.Categoria.First(cat => cat.id_categoria == IdCategoria);
                categoriaModificar.id_categoria = IdCategoria;
                categoriaModificar.nombre_categoria = Nombre;
                categoriaModificar.descripcion_categoria = Descripcion;
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
                Datos.Categoria categoriaEliminar = Connection.DBTienda.Categoria.First(cat => cat.id_categoria == IdCategoria);
                Connection.DBTienda.Categoria.Remove(categoriaEliminar);
                Connection.DBTienda.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Categoria> ListarConProducto()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            foreach (Datos.Categoria item in Connection.DBTienda.Categoria.Where(c => c.Producto.Count > 0).ToList())
            {
                listaCategoria.Add(new Categoria()
                {
                    IdCategoria = item.id_categoria,
                    Nombre = item.nombre_categoria,
                    Descripcion = item.descripcion_categoria,
                });
            }
            return listaCategoria;
        }
        
        public List<Categoria> ListarSinProducto()
        {
            List<Categoria> lista = new List<Categoria>();
            foreach (Datos.Categoria item in Connection.DBTienda.Categoria.Where(c => c.Producto.Count == 0).ToList())
            {
                lista.Add(new Categoria()
                {
                    IdCategoria = item.id_categoria,
                    Nombre = item.nombre_categoria,
                    Descripcion = item.descripcion_categoria,
                });
            }
            return lista;
        }

        public List<Categoria> Listartodos()
        {
            List<Categoria> listaT = new List<Categoria>();
            foreach (Datos.Categoria item in Connection.DBTienda.Categoria.ToList())
            {
                listaT.Add(new Categoria()
                {
                    IdCategoria = item.id_categoria,
                    Nombre = item.nombre_categoria,
                    Descripcion = item.descripcion_categoria,
                });
            }
            return listaT;
        }
    }
}
