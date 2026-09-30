namespace Proyecto_MySQL_1.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Sobrescribimos ToString para que el ComboBox muestre el nombre del empleado directamente
        public override string ToString() => Name;
    }
}