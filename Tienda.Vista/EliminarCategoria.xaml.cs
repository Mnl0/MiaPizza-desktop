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
    /// Lógica de interacción para EliminarCategoria.xaml
    /// </summary>
    public partial class EliminarCategoria : Window
    {
        public int IdCategoria { get; set; }
        
        public EliminarCategoria ( int idC)
        {
            InitializeComponent();

            IdCategoria = idC;

            Negocio.Categoria categoriaE = new Negocio.Categoria()
            {
                IdCategoria = IdCategoria,
            };
            if (categoriaE.Buscar())
            {
                txt_nombre_c.Text = categoriaE.Nombre;
                txt_descripcion_c.Text = categoriaE.Descripcion;
            }
        }

        private void btn_confirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.Categoria categoriaE = new Negocio.Categoria()
                {
                    IdCategoria = IdCategoria,
                };
                 MessageBoxResult respuest = MessageBox.Show("Desea eliminar la siguiente categoria", "Eliminar Categoria", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (respuest == MessageBoxResult.Yes)
                {
                    if (categoriaE.Eliminar())
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar Categoria");
            }
        }

        private void bnt_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
