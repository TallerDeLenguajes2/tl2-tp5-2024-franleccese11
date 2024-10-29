using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SQLitePCL;

public class ProductoRepository 
{
    private string connectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductoRepository(){}

    public List<Producto> GetAllProducts()
    {
        List<Producto> listaProductos = new List<Producto>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string queryString = @"SELECT * FROM Productos;";
            SqliteCommand command = new SqliteCommand(queryString,connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var producto = new Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["precio"]);
                    listaProductos.Add(producto);
                }
            }
            connection.Close();
        }
        return listaProductos;
    }

    public void InsertProducto(Producto producto)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "INSERT INTO productos(Descripcion, Precio) VALUES (@Descripcion,@Precio)";
                SqliteCommand command = new(queryString,connection);
                command.Parameters.AddWithValue("@Descripcion",producto.Descripcion);
                command.Parameters.AddWithValue("@Precio",producto.Precio);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            
            throw new Exception("Error al insertar el producto:",ex);
        }

    }


    public void UpdateProducto(int id, Producto producto)
    {
       try
       {
         using (SqliteConnection connection = new(connectionString))
         {
             connection.Open();
             string queryStr = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto= @id";
             SqliteCommand command = new(queryStr,connection);
             command.Parameters.AddWithValue("@Descripcion",producto.Descripcion);
             command.Parameters.AddWithValue("@Precio",producto.Precio);
             command.Parameters.AddWithValue("@id",id);
             command.ExecuteNonQuery();
             connection.Close();
         }
 
       }
       catch (SqliteException ex)
       {
        throw new Exception("error al actualizar el producto",ex);
       }
    }

    


}