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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tienda.Negocio;

namespace Tienda.Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int idc = 0;
        public int? codigoCarro = 0;

        public MainWindow()
        {
            InitializeComponent();

            ComboBoxCategoria();
            ComboBoxProducto();
            ComboboxCategoriaCrearP();

            DgvCategoria();
            DgvProducto();
            DgvPedido();

            ComboBoxModificarPedido();

            txt_telefono.Text = "+569";
            //desactivo crear cliente
            txt_direccion.IsEnabled = false;
            txt_n_calle.IsEnabled = false;
            txt_nombre_cliente.IsEnabled = false;
            txt_apellido_cliente.IsEnabled = false;
            btn_agregar_cliente.IsEnabled = false;
            //desactivo carro compra
            txt_cliente_pedido.IsEnabled = false;
            txt_cliente_pedido_nombre.IsEnabled = false;
            txt_cantidad.IsEnabled = false;
            btn_agregar_prod.IsEnabled = false;
            cb_tipos_productos.IsEnabled = false;
            cb_productos.IsEnabled = false;
            btn_generar_pedido.IsEnabled = false;
            txt_codigo_carro.IsEnabled = false;

            Boleta();
        }

        public void Boleta()
        {
            Pedido p = new Pedido();
            var a = p.Listar();
            if (a.Count != 0)
            {
                var b = a.Last();
                codigoCarro = b.CodigoCarro + 1;
                txt_codigo_carro.Text = codigoCarro.ToString();
            }
            else
            {
                txt_codigo_carro.Text = "1";
            }
        }

        public void DgvCategoria()
        {
            Categoria categoria = new Categoria();
            dg_lista_categoria.ItemsSource = categoria.Listartodos();
            dg_lista_categoria.Items.Refresh();
        }

        public void DgvProducto()
        {
            Producto p = new Producto();
            dgv_lista_productos.ItemsSource = p.ListarP();
            dgv_lista_productos.Items.Refresh();
        }

        public void DgvPedido()
        {
            Pedido p = new Pedido();
            dgv_lista_pedido.ItemsSource = p.Listar();
            dgv_lista_pedido.Items.Refresh();
        }

        public void DgvListaProducto()
        {
            Carro_Compra listaCC = new Carro_Compra();
            dgv_lista_producto.ItemsSource = listaCC.ListarCodigoCarro(int.Parse(txt_codigo_carro.Text));
            dgv_lista_producto.Items.Refresh();
        }

        public void ComboBoxCategoria()
        {
            Categoria listaCategoria = new Categoria();
            cb_categoria_modificar.ItemsSource = listaCategoria.Listartodos();
            cb_categoria_modificar.DisplayMemberPath = "Nombre";
            cb_categoria_modificar.SelectedValuePath = "IdCategoria";
            cb_categoria_modificar.SelectedIndex = -1;

            cb_categoria_p.ItemsSource = listaCategoria.Listartodos();
            cb_categoria_p.DisplayMemberPath = "Nombre";
            cb_categoria_p.SelectedValuePath = "IdCategoria";
            cb_categoria_p.SelectedIndex = -1;

            cb_categoria_eliminar.ItemsSource = listaCategoria.ListarSinProducto();
            cb_categoria_eliminar.DisplayMemberPath = "Nombre";
            cb_categoria_eliminar.SelectedValuePath = "IdCategoria";
            cb_categoria_eliminar.SelectedIndex = -1;
        }

        public void ComboboxCategoriaCrearP()
        {
            Categoria listaC = new Categoria();
            cb_tipos_productos.ItemsSource = listaC.ListarConProducto();
            cb_tipos_productos.DisplayMemberPath = "Nombre";
            cb_tipos_productos.SelectedValuePath = "IdCategoria";
            cb_tipos_productos.SelectedIndex = -1;
        }

        public void ComboBoxProducto()
        {
            Producto listaProducto = new Producto();
            cb_producto_modificar.ItemsSource = listaProducto.Listar();
            cb_producto_modificar.DisplayMemberPath = "NombreP";
            cb_producto_modificar.SelectedValuePath = "IdProducto";
            cb_producto_modificar.SelectedIndex = -1;

            cb_producto_eliminar.ItemsSource = listaProducto.Listar();
            cb_producto_eliminar.DisplayMemberPath = "NombreP";
            cb_producto_eliminar.SelectedValuePath = "IdProducto";
            cb_producto_eliminar.SelectedIndex = -1;
        }

        public void ComboBoxPedidio()
        {
            Producto listaP = new Producto();
            cb_productos.ItemsSource = listaP.ListarDos(idc);
            cb_productos.DisplayMemberPath = "NombreP";
            cb_producto_modificar.SelectedValuePath = "IdProducto";
            cb_producto_modificar.SelectedIndex = -1;
        }

        public void ComboBoxModificarPedido()
        {
            Pedido pedido = new Pedido();
            cb_estado_pedido.ItemsSource = pedido.Listar();
            cb_estado_pedido.DisplayMemberPath = "IdPedido ";
            cb_estado_pedido.SelectedValuePath = "IdPedido";
            cb_estado_pedido.SelectedIndex = -1;
        }

        private void btn_agregar_categoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_nombre_categoria.Text == "")
                {
                    validacion += "\nDebe ingresar un nombre a la categoria";
                }
                if (txt_descripcion_categoria.Text == "")
                {
                    validacion += "\nDebe ingresar una descripcion a la categoria";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Agregar Categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Categoria categoriaNueva = new Categoria()
                {
                    Nombre = txt_nombre_categoria.Text,
                    Descripcion = txt_descripcion_categoria.Text,
                };
                if (categoriaNueva.Crear())
                {
                    DgvCategoria();
                    ComboBoxCategoria();
                    txt_nombre_categoria.Clear();
                    txt_descripcion_categoria.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agreagar Categoria");
            }
        }

        private void btn_modificar_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (cb_categoria_modificar.SelectedIndex != -1)
            {
                int idc = ((Categoria)cb_categoria_modificar.SelectedItem).IdCategoria;
                ModificarCategoria vistaModificarC = new ModificarCategoria(idc);
                vistaModificarC.Owner = this;
                vistaModificarC.ShowDialog();
                DgvCategoria();
                ComboBoxCategoria();
            }
            else
            {
                MessageBox.Show("Seleccione la categoria que desea modificar", "Modificar Categoria", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        private void btn_eliminar_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (cb_categoria_eliminar.SelectedIndex != -1)
            {
                int idc = ((Categoria)cb_categoria_eliminar.SelectedItem).IdCategoria;
                EliminarCategoria vistaEliminarC = new EliminarCategoria(idc);
                vistaEliminarC.Owner = this;
                vistaEliminarC.ShowDialog();
                DgvCategoria();
                ComboBoxCategoria();
            }
            else
            {
                MessageBox.Show("Seleccione la Categoria que desea eliminar", "Eliminar Categoria", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        private void btn_agregar_producto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_nombre_p.Text == "")
                {
                    validacion += "\nDebe ingresar nombre al producto";
                }
                if (txt_nombre_p.Text.Length < 5)
                {
                    validacion += "\nNombre producto debe tener min 5 caracteres";
                }
                if (txt_precio_p.Text == "")
                {
                    validacion += "\nDebe ingresar el precio al producto";
                }
                if (txt_precio_p.Text.Length < 3)
                {
                    validacion += "\nVerifique el precio ingresado";
                }
                if (txt_descripcion_p.Text == "")
                {
                    validacion += "\nDebe ingresar una descripcion al producto";
                }
                if (cb_categoria_p.SelectedIndex == -1)
                {
                    validacion += "\nDebe asignale una categoria al producto";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Agregar Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Producto productoNuevo = new Producto()
                {
                    NombreP = txt_nombre_p.Text,
                    Precio = int.Parse(txt_precio_p.Text),
                    DescripcionP = txt_descripcion_p.Text,
                    IdCategoria = ((Categoria)cb_categoria_p.SelectedItem).IdCategoria,
                };
                if (productoNuevo.Crear())
                {
                    DgvProducto();
                    ComboBoxProducto();
                    ComboboxCategoriaCrearP();
                    txt_nombre_p.Clear();
                    txt_precio_p.Clear();
                    txt_descripcion_p.Clear();
                    cb_categoria_p.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar Producto");
            }
        }

        private void btn_modificar_producto_Click(object sender, RoutedEventArgs e)
        {
            if (cb_producto_modificar.SelectedIndex != -1)
            {
                int idp = ((Producto)cb_producto_modificar.SelectedItem).IdProducto;
                ModificarProducto vistaModificarProd = new ModificarProducto(idp);
                vistaModificarProd.Owner = this;
                vistaModificarProd.ShowDialog();
                DgvProducto();
                ComboBoxProducto();
            }
            else
            {
                MessageBox.Show("Seleccione el Producto a modificar", "Modificar Producto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_eliminar_producto_Click(object sender, RoutedEventArgs e)
        {
            if (cb_producto_eliminar.SelectedIndex != -1)
            {
                int idp = ((Producto)cb_producto_eliminar.SelectedItem).IdProducto;
                EliminarProducto vistaEliminarProd = new EliminarProducto(idp);
                vistaEliminarProd.Owner = this;
                vistaEliminarProd.ShowDialog();
                DgvProducto();
                ComboBoxProducto();
            }
            else
            {
                MessageBox.Show("Seleccionar un producto", "Eliminar Producto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_agregar_cliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacion = "";
                if (txt_nombre_cliente.Text == "")
                {
                    validacion += "\nDebe ingresar el nombre del cliente";
                }
                if (txt_apellido_cliente.Text == "")
                {
                    validacion += "\nDebe ingresar el apellido del cliente";
                }
                if (txt_direccion.Text == "")
                {
                    validacion += "\nDebe ingresar la direccion del cliente";
                }
                if (txt_n_calle.Text == "")
                {
                    validacion += "\nDebe ingresar numero de calle de la direccion";
                }
                if (txt_telefono.Text == "")
                {
                    validacion += "\nDebe ingresar numero telefono del direccion";
                }
                if (validacion != "")
                {
                    MessageBox.Show(validacion, "Generar Pedido", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Cliente clienteN = new Cliente()
                {
                    TelefonoC = txt_telefono.Text,
                    NombreC = txt_nombre_cliente.Text,
                    ApellidoC = txt_apellido_cliente.Text,
                    CalleC = txt_direccion.Text,
                    NumeroC = txt_n_calle.Text,
                };
                if (clienteN.Crear())
                {
                    clienteN.Buscar(txt_telefono.Text);
                    MessageBox.Show("cliente agregado con exito", "Generar pedido", MessageBoxButton.OK, MessageBoxImage.Information);
                    txt_cliente_pedido.Text = clienteN.IdCliente.ToString();
                    txt_cliente_pedido_nombre.Text = clienteN.NombreC.ToString();

                    //limpio crear cliente
                    txt_nombre_cliente.Text = "";
                    txt_apellido_cliente.Text = "";
                    txt_direccion.Text = "";
                    txt_n_calle.Text = "";
                    txt_cantidad.Text = "";
                    //desactivo crear cliente
                    txt_direccion.IsEnabled = false;
                    txt_n_calle.IsEnabled = false;
                    txt_nombre_cliente.IsEnabled = false;
                    txt_apellido_cliente.IsEnabled = false;
                    btn_agregar_cliente.IsEnabled = false;
                    //activo carro compra
                    cb_productos.IsEnabled = true;
                    cb_tipos_productos.IsEnabled = true;
                    txt_cantidad.IsEnabled = true;
                    btn_agregar_prod.IsEnabled = true;
                    cb_productos.SelectedIndex = -1;
                    cb_tipos_productos.SelectedIndex = -1;
                    txt_cantidad.Text = "";
                    //desactivo verificar
                    txt_telefono.IsEnabled = false;
                    btn_buscar_cliente.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar Pedido", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_agregar_prod_Click(object sender, RoutedEventArgs e)
        {
            string validacion = "";
            if (txt_cliente_pedido.Text.Length == 0)
            {
                validacion += "\nId cliente no puede estar vacio";
            }
            if (cb_productos.SelectedIndex == -1)
            {
                validacion += "\nDebe seleccionar un producto";
            }
            if (txt_cantidad.Text.Length == 0)
            {
                validacion += "\nDebe ingresar la cantidad";
            }
            if (validacion != "")
            {
                MessageBox.Show(validacion, "Carro Compra", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Carro_Compra carroCompra = new Carro_Compra()
            {
                IdCliente = int.Parse(txt_cliente_pedido.Text),
                IdProducto = ((Producto)cb_productos.SelectedItem).IdProducto,
                Cantidad = int.Parse(txt_cantidad.Text),
                CodigoCarro = int.Parse(txt_codigo_carro.Text),
                ValorUnitario = ((Producto)cb_productos.SelectedItem).Precio,
            };
            string validacion2 = "";
            if (carroCompra.Cantidad <= 0)
            {
                validacion2 += "\nla cantidad debe ser mayor a cero";
            }
            if (carroCompra.CodigoCarro <= 0)
            {
                txt_codigo_carro.Text = "1";
            }
            if (validacion2 != "")
            {
                MessageBox.Show(validacion2, "Carro compra", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (carroCompra.Crear())
            {
                DgvListaProducto();
                btn_generar_pedido.IsEnabled = true;

                var suma = carroCompra.ListarCodigoCarro(int.Parse(txt_codigo_carro.Text));
                txt_total.Text = suma.Sum(x => x.SubTotal).ToString();
            }
        }

        private void btn_generar_pedido_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string validacionDos = "";
                if (cb_tipos_productos.SelectedIndex == -1)
                {
                    validacionDos += "\nDebe seleccionar un tipo de producto";
                }
                if (cb_productos.SelectedIndex == -1)
                {
                    validacionDos += "\nDebe seleccionar un producto";
                }
                if (txt_cantidad.Text == "")
                {
                    validacionDos += "\nDebe indicar la cantidad de productos";
                }
                if (validacionDos != "")
                {
                    MessageBox.Show(validacionDos, "Generar Pedido", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Carro_Compra carro = new Carro_Compra();
                int idcarro = 0;
                if (carro.Buscar(int.Parse(txt_codigo_carro.Text)))
                {
                    idcarro = carro.IdCarroCompra;
                }
                string fecha = DateTime.Now.ToShortDateString();
                string hora = DateTime.Now.ToShortTimeString();
                Pedido pedido = new Pedido
                {
                    nombreC = txt_cliente_pedido_nombre.Text,
                    Estado = "En preparacion",
                    Fecha = fecha,
                    Hora = hora,
                    CodigoCarro = int.Parse(txt_codigo_carro.Text),
                    IdCarro = idcarro,
                    TotalPrecio = int.Parse(txt_total.Text),
                };
                if (pedido.Crear())
                {
                    DgvPedido();
                    ComboBoxModificarPedido();
                    //activo buscar cliente limpio formulario
                    txt_telefono.Text = "+569";
                    btn_buscar_cliente.IsEnabled = true;
                    txt_telefono.IsEnabled = true;
                    txt_cantidad.Text = "";
                    txt_cliente_pedido.Text = "";
                    txt_cliente_pedido_nombre.Text = "";
                    cb_productos.IsEnabled = false;
                    cb_tipos_productos.IsEnabled = false;
                    txt_cantidad.IsEnabled = false;
                    btn_agregar_prod.IsEnabled = false;
                    btn_generar_pedido.IsEnabled = false;
                    txt_total.Text = "";
                    Boleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar Pedido");
            }
        }

        private void cb_tipos_productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_tipos_productos.Focus() && cb_productos.Focus())
            {
                idc = ((Categoria)cb_tipos_productos.SelectedItem).IdCategoria;
                ComboBoxPedidio();
            }
            return;
        }

        private void txt_nombre_categoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.B || e.Key >= Key.C && e.Key <= Key.D || e.Key == Key.Tab || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.N)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_descripcion_categoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.B || e.Key >= Key.C && e.Key <= Key.D || e.Key == Key.Tab || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.N)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_nombre_p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.B || e.Key >= Key.C && e.Key <= Key.D || e.Key == Key.Tab || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.N)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_descripcion_p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.B || e.Key >= Key.C && e.Key <= Key.D || e.Key == Key.Tab || e.Key == Key.E || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.N)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_precio_p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public void LimpiarAdmCat()
        {
            txt_nombre_categoria.Text = "";
            txt_descripcion_categoria.Text = "";
            cb_categoria_modificar.SelectedIndex = -1;
            cb_categoria_eliminar.SelectedIndex = -1;
        }

        public void LimpiarAdmProd()
        {
            txt_nombre_p.Text = "";
            txt_precio_p.Text = "";
            txt_descripcion_p.Text = "";
            cb_categoria_p.SelectedIndex = -1;
            cb_producto_modificar.SelectedIndex = -1;
            cb_producto_eliminar.SelectedIndex = -1;
        }

        public void LimpiarAdmPedido()
        {
            txt_nombre_cliente.Text = "";
            txt_apellido_cliente.Text = "";
            txt_direccion.Text = "";
            txt_n_calle.Text = "";
            txt_telefono.Text = "";
            txt_cantidad.Text = "";
            cb_tipos_productos.SelectedIndex = -1;
            cb_productos.SelectedIndex = -1;
            txt_cliente_pedido.Text = "";
            txt_cliente_pedido_nombre.Text = "";
            txt_total.Text = "";
            cb_estado_pedido.SelectedIndex = -1;
        }

        private void AdmPedido(object sender, RoutedEventArgs e)
        {
            LimpiarAdmCat();
            LimpiarAdmProd();
        }

        private void AdmCategoria(object sender, RoutedEventArgs e)
        {
            LimpiarAdmPedido();
            LimpiarAdmProd();
        }

        private void AdmProducto(object sender, RoutedEventArgs e)
        {
            LimpiarAdmCat();
            LimpiarAdmPedido();
        }

        private void btn_modificar_estado_Click(object sender, RoutedEventArgs e)
        {
            if (cb_estado_pedido.SelectedIndex != -1)
            {
                int idp = ((Pedido)cb_estado_pedido.SelectedItem).IdPedido;
                Modificar_Pedido vistaModificarPedido = new Modificar_Pedido(idp); 
                vistaModificarPedido.Owner = this;
                vistaModificarPedido.ShowDialog();
                DgvPedido();
            }
            else
            {
                MessageBox.Show("Seleccione el pedido a modificar", "Modificar Pedido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_buscar_cliente_Click(object sender, RoutedEventArgs e)
        {
            if (txt_telefono.Text.StartsWith("+569")) 
            {
                if (txt_telefono.Text.Length == 12)
                {
                    Cliente cliente = new Cliente();
                    if (cliente.Buscar(txt_telefono.Text))
                    {
                        txt_nombre_cliente.Text = cliente.NombreC;
                        txt_direccion.Text = cliente.CalleC;
                        txt_n_calle.Text = cliente.NumeroC;
                        txt_apellido_cliente.Text = cliente.ApellidoC;
                        txt_cliente_pedido.Text = cliente.IdCliente.ToString();
                        txt_cliente_pedido_nombre.Text = cliente.NombreC.ToString();
                        //activo carro de compra
                        cb_productos.IsEnabled = true;
                        cb_tipos_productos.IsEnabled = true;
                        txt_cantidad.IsEnabled = true;
                        btn_agregar_prod.IsEnabled = true;
                        //desactivo crear cliente
                        txt_direccion.IsEnabled = false;
                        txt_n_calle.IsEnabled = false;
                        txt_nombre_cliente.IsEnabled = false;
                        txt_apellido_cliente.IsEnabled = false;
                        btn_agregar_cliente.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Cliente no regristrado", "Buscar Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                        //activo crear cliente
                        txt_direccion.IsEnabled = true;
                        txt_n_calle.IsEnabled = true;
                        txt_nombre_cliente.IsEnabled = true;
                        txt_apellido_cliente.IsEnabled = true;
                        btn_agregar_cliente.IsEnabled = true;
                        //limpio crear cliente
                        txt_nombre_cliente.Text = "";
                        txt_apellido_cliente.Text = "";
                        txt_direccion.Text = "";
                        txt_n_calle.Text = "";
                        txt_cantidad.Text = "";
                        //desactivo carro compra
                        cb_tipos_productos.IsEnabled = false;
                        cb_productos.IsEnabled = false;
                        txt_cantidad.IsEnabled = false;
                        btn_agregar_prod.IsEnabled = false;
                        //limpio carro compra
                        txt_cliente_pedido.Text = "";
                        txt_cliente_pedido_nombre.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un numero valido +56999887766", "Agregar Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Formato Telefonico incorrecto +569...", "Agregar Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txt_telefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab || e.Key == Key.K || e.Key == Key.OemPlus)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            txt_telefono.Text = "+569";
            //desactivo crear cliente
            txt_direccion.IsEnabled = false;
            txt_n_calle.IsEnabled = false;
            txt_nombre_cliente.IsEnabled = false;
            txt_apellido_cliente.IsEnabled = false;
            btn_agregar_cliente.IsEnabled = false;
            //desactivo carro compra
            txt_cliente_pedido.IsEnabled = false;
            txt_cliente_pedido_nombre.IsEnabled = false;
            txt_cantidad.IsEnabled = false;
            btn_agregar_prod.IsEnabled = false;
            cb_tipos_productos.IsEnabled = false;
            cb_productos.IsEnabled = false;
            btn_generar_pedido.IsEnabled = false;
            //limpiar cliente
            txt_direccion.Text = "";
            txt_n_calle.Text = "";
            txt_nombre_cliente.Text = "";
            txt_apellido_cliente.Text = "";
            txt_cliente_pedido.Text = "";
            txt_cliente_pedido_nombre.Text = "";
            txt_codigo_carro.Text = "0";
            txt_cantidad.Text = "0";

            txt_codigo_carro.Text = codigoCarro.ToString();
        }
    }

}
