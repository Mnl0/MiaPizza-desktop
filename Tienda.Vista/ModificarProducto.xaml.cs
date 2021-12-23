using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tienda.Vista
{
    /// <summary>
    /// Lógica de interacción para ModificarProducto.xaml
    /// </summary>
    public partial class ModificarProducto : Window
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }

        public ModificarProducto(int idp)
        {
            InitializeComponent();
            IdProducto = idp;

            Negocio.Producto productoM = new Negocio.Producto()
            {
                IdProducto = IdProducto,
            };
            if (productoM.Buscar())
            {
                txt_nompre_producto.Text = productoM.NombreP;
                txt_precio.Text = productoM.Precio.ToString();
                txt_descripcion.Text = productoM.DescripcionP;
                IdCategoria = productoM.IdCategoria;
            }
        }

        private void btn_modificar_prod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_nompre_producto.Text == "")
                {
                    validacion += "\nDebe ingresar un nombre al producto";
                }
                if (txt_precio.Text == "")
                {
                    validacion += "\nDebe ingresar un precio al producto";
                }
                if (txt_descripcion.Text == "")
                {
                    validacion += "\nDebe ingresar una descripcion al producto";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Modificar Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Negocio.Producto productoM = new Negocio.Producto()
                {
                    IdProducto = IdProducto,
                    IdCategoria = IdCategoria,
                };
                productoM.NombreP = txt_nompre_producto.Text;
                productoM.Precio = int.Parse(txt_precio.Text);
                productoM.DescripcionP = txt_descripcion.Text;
                if (productoM.Actulizar())
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Modificar Producto");
            }
        }

        private void btn_cancelar_prod_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txt_precio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
