using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MySqlConnector;

namespace DataAccess.DBAccess;

public class MySqlDataAccess : IMySqlDataAccess
{
    private readonly IConfiguration _config;
    public MySqlDataAccess(IConfiguration config)
    {
        _config = config;
    }


    public async Task<IEnumerable<T>> LoadData<T, U>(
        string query,
        U parameters,
        string connectionId = "Default")
    {
        // create connection. 
        //Shuts down the connection automaticaly with "using"
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(query, parameters);

    }

    public async Task SaveData<T>(
        string query,
        T parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new MySqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(query, parameters);

    }

}
