using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace Tarea1.Pages.Empleados
{
    public class CrearModel : PageModel
    {
        public EmpleadoO nuevoEmpleado = new EmpleadoO();
        public String errorMessage = ""; public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            nuevoEmpleado.Nombre = Request.Form["Nombre"].ToString();
            String nSalario = Request.Form["Salario"].ToString();
            

            if (nuevoEmpleado.Nombre.Length == 0 || nSalario.Length == 0)
            {
                errorMessage = "Debe llenar todos los campos";
                return;
            }

			nuevoEmpleado.Salario = decimal.Parse(nSalario);

            try
            {
                String connectionString = "Server=localhost\\MSSQLSERVER01;Database=Tarea1;Trusted_Connection=Yes;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT Empleado (Nombre, Salario) VALUES " +
                        "(@Nombre, @Salario);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nuevoEmpleado.Nombre);
                        command.Parameters.AddWithValue("@Salario", nuevoEmpleado.Salario);

                        command.ExecuteNonQuery();
                    }
                }
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
			nuevoEmpleado.Nombre = ""; nuevoEmpleado.Salario = 0;
            successMessage = "Se agregó correctamente";

            Response.Redirect("/Empleados/Index");
		}
    }
}
