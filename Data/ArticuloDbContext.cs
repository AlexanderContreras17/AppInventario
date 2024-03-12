using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppInventario.Models;
using Microsoft.Data.Sqlite;
namespace AppInventario.Data
{
    public class ArticuloDbContext
    {
        private const string _connectionString="Data Source=articulos.db";
        public ArticuloDbContext() 
        { 
         using ( var connection = new SqliteConnection( _connectionString ) )
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"CREATE IF NOT EXIST articulos(
                                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            descripcion VARCHAR(60) NOT NULL,
                                            precio DECIMAL NOT NULL,
                                            existencia INTEGER NOT NULL
                            );";
                command.ExecuteNonQuery();
                command.ExecuteReader(); // para cuando te vas a traer registros y el Scalar para traer solo 1 valor
            }

        }
        public async Task Agregar(Articulo articulo)
        {
            using ( var connection = new SqliteConnection ( _connectionString ) )
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO articulos
                                           (descripcion, precio, existencia)
                                            values( $descripcion, $precio, $existencia)";
            }
        }
    }
}
