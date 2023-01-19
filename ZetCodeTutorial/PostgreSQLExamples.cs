using System.Security.Cryptography;
using Npgsql;

namespace ZetCodeTutorial;

public static class PostgreSQLExamples
{
    public static void PostgreSQL_Version()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=test";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT version()";

        using var sqlCommand = new NpgsqlCommand(sql, connection);

        var version = sqlCommand.ExecuteScalar()?.ToString();

        Console.WriteLine($"PostgreSQL version: {version}");
    }

    public static void PostgreSQL_CreateTable()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=test";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var sqlCommand = new NpgsqlCommand();
        sqlCommand.Connection = connection;

        sqlCommand.CommandText = "DROP TABLE IF EXISTS cars";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Audi', 52642)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes', 57127)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda', 9000)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo', 29000)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley', 350000)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen', 21000)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer', 41400)";
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen', 21600)";
        sqlCommand.ExecuteNonQuery();
        
        Console.WriteLine("Table cars created");
    }

    public static void PostgreSQL_PreparedStatements()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=test";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "INSERT INTO cars(name, price) VALUES(@name, @price)";
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("name", "BMW");
        command.Parameters.AddWithValue("price", 36600);
        command.Prepare();

        command.ExecuteNonQuery();

        Console.WriteLine("row inserted");
    }

    public static void PostgreSQL_NpgsqlDataReader()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=test";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM cars";
        using var command = new NpgsqlCommand(sql, connection);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
        }
    }

    public static void PostgreSQL_ColumnHeaders()
    {
        var connectionString = "Host=localhost;Username=postgres;Password=admin;Database=test";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT * FROM cars";

        using var command = new NpgsqlCommand(sql, connection);

        using NpgsqlDataReader reader = command.ExecuteReader();
        Console.WriteLine($"{reader.GetName(0), -4} {reader.GetName(0), -10} {reader.GetName(2), 10}");

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0), -4} {reader.GetString(1), -10} {reader.GetInt32(2), 10}");
        }
    }
}