using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP5.Repositorio
{
    public class PresupuestoRepository:IPresupuestoRepository
    {
        private readonly string connectionString = @"Data Source=db/Tienda.db;Cache=Shared";

        public void CrearPresupuesto(Presupuesto presupuesto) //en cual capa de abstraccion debo controlar que no sea NULL?
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    connection.Open();
                    string queryStr= @" INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario,@FechaCreacion)";
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@NombreDestinatario",presupuesto.NombreDestinatario);
                    command.Parameters.AddWithValue("@FechaCreacion",presupuesto.Fecha);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqliteException ex)
            {
                
                throw new Exception("Error al conectar con la base de datos",ex);
            }
        }


        public List<Presupuesto> ListarPresupuestos()
        {
            try
            {
                using (SqliteConnection connection=new(connectionString))
                {
                    connection.Open();
                    string queryStr="SELECT idPresupuesto,NombreDestinatario,FechaCreacion FROM Presupuestos";
                    var listaPresupuestos = new List<Presupuesto>();
                    SqliteCommand command = new(queryStr,connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        Presupuesto presupuesto = new()
                        {
                            IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                            Fecha = (DateTime)reader["FechaCreacion"],
                            NombreDestinatario = reader["NombreDestinatario"].ToString()
                        };
                        listaPresupuestos.Add(presupuesto);
                    }
                    connection.Close();
                    return listaPresupuestos;
                }
            }
            catch (SqliteException ex)
            {
                
                throw new Exception("Error en la conexion con la Base de datos",ex);
            }
        }

        public List<PresupuestoDetalle> ObtenerDetalle(int id)
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    connection.Open();
                    string queryStr = "SELECT idPresupuesto, idProducto, Cantidad FROM PresupuestoDetalle WHERE idPresupuesto= @id";
                    var listaPresupuestoDetalle = new List <PresupuestoDetalle>();
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@id",id);
                    var listaDetalle = new List<PresupuestoDetalle>();
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var presupuestoDetalle = new PresupuestoDetalle();
                            presupuestoDetalle.AsignarProducto(Convert.ToInt32(reader["idProducto"]));
                            presupuestoDetalle.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                            listaDetalle.Add(presupuestoDetalle);
                        }
                        
                    }
                    connection.Close();
                    return listaDetalle;
                }
            }
            catch (SqliteException ex)
            {
                
                throw new Exception("Error en la conexion con la Base de datos",ex);
            }
        }

        public void EliminarPresupuesto(int id)
        {
            try
            {
                using (SqliteConnection connection = new(connectionString))
                {
                    connection.Open();
                    string queryStr = @"DELETE FROM Presupuesto WHERE idPresupuesto=@id";
                    SqliteCommand command = new(queryStr,connection);
                    command.Parameters.AddWithValue("@id",id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqliteException ex)
            {
                
                throw new("Error al conectar con la base de datos",ex);
            }
        }




    }
}