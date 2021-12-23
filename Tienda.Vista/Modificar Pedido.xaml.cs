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
    /// Lógica de interacción para Modificar_Pedido.xaml
    /// </summary>
    public partial class Modificar_Pedido : Window
    {
        public int Idp { get; set; }

        public Modificar_Pedido(int idp)
        {
            InitializeComponent();
            idp = idp;

            Negocio.Pedido pedido = new Negocio.Pedido()
            {
                IdPedido = idp,
            };
            if (pedido.BuscarPedido())
            {
                txt_id_pedido.Text = pedido.IdPedido.ToString();
                txt_nombre_cliente.Text = pedido.nombreC;
                txt_estado_cliente.Text = pedido.Estado;
                txt_fecha_pedido.Text = pedido.Fecha.ToString();
                txt_id_carro.Text = pedido.IdCarro.ToString();
                txt_codigo_carro_.Text = pedido.CodigoCarro.ToString();
                txt_precio_total.Text = pedido.TotalPrecio.ToString();
            }

            txt_id_pedido.IsEnabled = false;
            txt_nombre_cliente.IsEnabled = false;
            txt_fecha_pedido.IsEnabled = false;
            txt_id_carro.IsEnabled = false;
            txt_codigo_carro_.IsEnabled = false;
            txt_precio_total.IsEnabled = false;
            txt_estado_cliente.IsEnabled = false;

            cb_estado_modificar.Items.Add("En Reparto");
            cb_estado_modificar.Items.Add("Entregado");
            cb_estado_modificar.Items.Add("Anulado");

        }

        private void btn_modificar_pedido_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_estado_cliente.Text  == "")
                {
                    validacion += "\nDebe mencionar un estado";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Modificar Pedido", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Negocio.Pedido pedido = new Negocio.Pedido()
                {
                    IdPedido = int.Parse(txt_id_pedido.Text),
                    IdCarro = int.Parse(txt_id_carro.Text),
                    CodigoCarro = int.Parse(txt_codigo_carro_.Text),
                    Fecha = txt_fecha_pedido.Text,
                    nombreC = txt_nombre_cliente.Text,
                    TotalPrecio = int.Parse(txt_precio_total.Text)
                };
                pedido.Estado = cb_estado_modificar.SelectedItem.ToString();
                if (pedido.Actualizar())
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("llego aca");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Modificar Pedido");
            }
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
