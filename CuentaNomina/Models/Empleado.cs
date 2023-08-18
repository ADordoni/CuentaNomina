namespace CuentaNomina.Models
{
    public class Empleado
    {
        public string dni
        {
            get;
            set;
        }
        public string nombre
        {
            get;
            set;
        }
        public string domicilio
        {
            get;
            set;
        }
        public DateTime fechanac
        {
            get;
            set;
        }
        public DateTime fechaing { get; set; }
        public string puesto { get; set; }
        public int salario { get; set; }
        public string usuario { get; set; }
    }
}
