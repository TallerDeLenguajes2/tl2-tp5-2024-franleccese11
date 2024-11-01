using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP5.Repositorio
{
    public class ProductoRepository: IProductoRepository
    {
        private readonly string connectionString = @"Data Source=db/Tienda.db;Cache=Shared";

        public ProductoRepository(){}

        public bool ValidarID(int id)
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    string queryStr = @"SELECT COUNT(*) FROM Productos WHERE idProducto = @id";
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@id",id);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count>0;
                }
            }
            catch (SqliteException ex)
            {
                
                throw new ("",ex);
            }
        }

        public List<Producto> ListarProductos()
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
                        var producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["idProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToInt32(reader["precio"])
                        };
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


        public void UpdateProducto(Producto producto) //consultar el manejo de excepcion si no encuentra un producto para modificar
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
                    command.Parameters.AddWithValue("@id",producto.IdProducto);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
        
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error al actualizar el producto",ex);
            }
        }

        
        public Producto ObtenerProducto(int id)
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    connection.Open();
                    string queryStr = "SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @id";
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@id",id);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Producto producto = new();
                            producto.IdProducto = id;
                            producto.Descripcion = reader["Descripcion"].ToString();
                            producto.Precio = Convert.ToInt32(reader["Precio"]);
                            connection.Close();
                            return producto;
                        }else
                        {   
                            connection.Close();
                            return null;
                        }
                    }
        
                    
                }
            }
            catch (SqliteException ex)
            {
                
                throw new Exception("Error en la base de datos al obtener el producto ",ex);
            } 
            catch(Exception ex)
            {
                throw new Exception("Error al obtener el producto ",ex);
            }
        }


        public void EliminarProducto(int id)
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    string queryStr = @"DELETE FROM Productos WHERE idProducto = @id";
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@id",id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                }
            }
            catch (SqliteException ex)
            {
                
                throw new Exception("Error al conectar con la base de datos", ex);
            }
        }


    }
}