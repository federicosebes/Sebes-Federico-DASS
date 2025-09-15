using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace CrudAdo.Net
{
    public class RepositorioAlumno
    {
        private List<Alumno> ListaAlumnos;
        
        public RepositorioAlumno()
        {
            ListaAlumnos = new List<Alumno>();
        }


        public List<Alumno> listar()
        {
            string query = "SELECT * FROM Alumnos";

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena)) 
                {
                 Sqlcommand comando = new SqlCommand(query, conexion);
                 conexion.Open();

                 SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Alumno a = new Alumno();
                        a.Id = int.Parse(reader["id"].ToString());
                        a.Nombre = reader["nombre"].ToString();
                        a.Nota = int.Parse(reader["nota"].ToString());

                        ListaAlumnos.Add(a);
                    }
                    conexion.Close();

                    return ListaAlumnos;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los alumnos: " + ex.Message);
            }

        }

        public string Agregar(Alumno alumno)
        {
            string query = "INSERT INTO Alumnos(nombre, nota) values" + "(@nombre, @nota)";
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre", alumno.Nombre);
                comando.Parameters.AddWithValue("@nota", alumno.Nota);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    return "Alumno agregado correctamente";
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error inesperado en algo relacionado con la BD" + ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inesperado");
                }
            }
        }

        public string Modificar(Alumno alumno)
        {
            string query = "UPDATE Alumnos " +
                           "SET nombre = @nombre, nota = @nota " +
                           "WHERE id = @id";

            using (SqlConnection conexion = new SqlConnection(cadena))
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", alumno.Nombre);
                comando.Parameters.AddWithValue("@nota", alumno.Nota);
                comando.Parameters.AddWithValue("@id", alumno.Id);

                try
                {
                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();
                    conexion.Close();

                    if (filas == 0) return "No existe un alumno con ese id";
                    return "Alumno modificado correctamente";
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error inesperado en algo relacionado con la BD " + ex.Message, ex);
                }
                catch (Exception)
                {
                    throw new Exception("Error inesperado");
                }
            }
        }

        public string Eliminar(int id)
        {
            string query = "DELETE FROM Alumnos WHERE id = @id";

            using (SqlConnection conexion = new SqlConnection(cadena))
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);

                try
                {
                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();
                    conexion.Close();

                    if (filas == 0) return "No existe un alumno con ese id";
                    return "Alumno eliminado correctamente";
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error inesperado en algo relacionado con la BD " + ex.Message, ex);
                }
                catch (Exception)
                {
                    throw new Exception("Error inesperado");
                }
            }
        }
    }
}
