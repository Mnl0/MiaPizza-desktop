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
    /// Lógica de interacción para EliminarProducto.xaml
    /// </summary>
    public partial class EliminarProducto : Window
    {
        public int IdProducto { get; set; }

        public EliminarProducto(int idp)
        {
            InitializeComponent();

            IdProducto = idp;
            Negocio.Producto productoE = new Negocio.Producto()
            {
                IdProducto = IdProducto,
            };
            if (productoE.Buscar())
            {
                txt_nombre_prod.Text = productoE.NombreP;
                txt_precio_prod.Text = productoE.Precio.ToString();
                txt_descripcion_prod.Text = productoE.DescripcionP;
            }
        }

        private void btn_eliminar_prod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.Producto productoE = new Negocio.Producto()
                {
                    IdProducto = IdProducto,
                };
                MessageBoxResult result = MessageBox.Show("Desea eliminar el siguiente producto","Eliminar Producto",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (productoE.Eliminar()) 
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar Producto");
            }
        }

        private void btn_cancelar_prod_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
