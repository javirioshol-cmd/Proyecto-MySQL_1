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

        // Reemplaza los datos por los de tu servidor MySQL local
        private const string ConnectionString = "Server=localhost;Database=sakila;Uid=root;Pwd=1234;";

        public MainWindow()
        {
            InitializeComponent();
            _staffRepository = new StaffRepository(ConnectionString);
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
    }
}