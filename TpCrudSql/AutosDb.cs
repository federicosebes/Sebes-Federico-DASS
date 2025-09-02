using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TpCrudSql
{
    public class AutosDb
    {

        private string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CrudWindowsForm;Integrated Security=True;";
        public bool Ok()
        {

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<Auto> Get()
        {
            List<Auto> auto = new List<Auto>();

            string query = "select id,name,age from autos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Auto oAuto = new Auto();
                        oAuto.Id = reader.GetInt32(0);
                        oAuto.Name = reader.GetString(1);
                        oAuto.Age = reader.GetInt32(2);
                        auto.Add(oAuto);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos" + ex.Message);

                }
            }

            return auto;
        }

        public void AgregarAuto(string Name, int Age)
        {
            string query = "insert into autos (name, age) values" + "(@name, @age)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", Name);
                command.Parameters.AddWithValue("@age", Age);




                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos" + ex.Message);

                }
            }

        }

        public void EditarAuto(int id, string name, int age)
        {
            string query = "UPDATE autos SET name = @name, age = @age WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@age", age);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos: " + ex.Message);
                }
            }

        }


        public void EliminarAuto(int id)
        {
            string query = "DELETE FROM autos WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error en la base de datos: " + ex.Message);
                }
            }
        }
    }
}

    public class Auto 
    {
      public int Id { get; set; }   
      public string Name { get; set; }        
      public int Age { get; set; }
    
    }

}
