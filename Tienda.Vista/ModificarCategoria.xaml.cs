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
    /// Lógica de interacción para ModificarCategoria.xaml
    /// </summary>
    public partial class ModificarCategoria : Window
    {
        public int IdCategoria { get; set; }

        public ModificarCategoria(int idC)
        {
            InitializeComponent();

            IdCategoria = idC;

            Negocio.Categoria categoriaM = new Negocio.Categoria()
            {
                IdCategoria = IdCategoria,
            };
            if (categoriaM.Buscar())
            {
                txt_nombre_c.Text = categoriaM.Nombre;
                txt_descripcion_c.Text = categoriaM.Descripcion;
            }
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_nombre_c.Text == "")
                {
                    validacion += "\nDebe ingresar un nombre a la categoria";
                }
                if (txt_descripcion_c.Text == "")
                {
                    validacion += "\nDebe ingresar una descripcion a la categoria";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Modificar Categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Negocio.Categoria categoriaM = new Negocio.Categoria()
                {
                    IdCategoria = IdCategoria,
                };
                categoriaM.Nombre = txt_nombre_c.Text;
                categoriaM.Descripcion = txt_descripcion_c.Text;
                if (categoriaM.Actualizar())
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Modificar Categoria", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
