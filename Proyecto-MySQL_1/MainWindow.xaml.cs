using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using System.Windows.Controls;
using Proyecto_MySQL_1.Models;
using Proyecto_MySQL_1.Repositories;

namespace Proyecto_MySQL_1
{
    public partial class MainWindow : Window
    {
        private readonly StaffRepository _staffRepository;
        private readonly DatosEmpRepositories _repo;
        // Reemplaza los datos por los de tu servidor MySQL local
        private const string ConnectionString = "Server=localhost;Database=sakila;Uid=root;Pwd=1234;";

        public MainWindow()
        {
            InitializeComponent();
            _staffRepository = new StaffRepository(ConnectionString);
            _repo = new DatosEmpRepositories();
            CargarEmpleados();
        }

        private async void CargarEmpleados()
        {
            var empleados = await _staffRepository.GetAllStaffAsync();
            cmbEmpleados.ItemsSource = empleados;
        }

        private async void cmbEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEmpleados.SelectedItem is Staff empleadoSeleccionado)
            {
                int totalAlquileres = await _staffRepository.GetRentalCountByStaffIdAsync(empleadoSeleccionado.Id);
                txtTotalAlquileres.Text = totalAlquileres.ToString();
            }
        }
    
    private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtStaffId.Text.Trim(), out int id))
            {
                MessageBox.Show("Introduce un ID numérico válido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Datos empleado = _repo.ObtenerEmpleadoPorId(id);

                if (empleado != null)
                {
                    txtNombre.Text = empleado.FirstName;
                    txtApellidos.Text = empleado.LastName;
                    txtEmail.Text = empleado.Email;
                    txtUsuario.Text = empleado.Username;
                    txtStoreId.Text = empleado.StoreId.ToString();
                    chkActivo.IsChecked = empleado.Active;
                }
                else
                {
                    LimpiarFormulario();
                    MessageBox.Show($"No se encontró ningún empleado con el ID {id}.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar o consultar MySQL: {ex.Message}", "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtEmail.Clear();
            txtUsuario.Clear();
            txtStoreId.Clear();
            chkActivo.IsChecked = false;
        }
    }

}