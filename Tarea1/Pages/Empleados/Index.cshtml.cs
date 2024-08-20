using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Tarea1.Pages.Empleados
{
    public class IndexModel : PageModel
    {
        public List<EmpleadoO> EmpleadosL = new List<EmpleadoO>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=localhost\\MSSQLSERVER01;Database=Tarea1;Trusted_Connection=Yes;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Empleado";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) {
							while (reader.Read())
                            {
                                EmpleadoO nEmpleado = new EmpleadoO();
                                nEmpleado.id = reader.GetInt32(0);
                                nEmpleado.Nombre = reader.GetString(1);
                                nEmpleado.Salario = reader.GetDecimal(2);  

                                EmpleadosL.Add(nEmpleado);
                            }

						}
                        
                    }
                }

			}
            catch (Exception ex)
            {
                return;
            }
        }
    }

    public class EmpleadoO
    {
        public int id;
        public string Nombre;
        public decimal Salario;
    }
}
