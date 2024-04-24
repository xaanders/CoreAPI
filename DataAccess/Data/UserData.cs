using DataAccess.DBAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class UserData(IMySqlDataAccess db) : IUserData
{
    private readonly IMySqlDataAccess _db = db;

    public Task<IEnumerable<UserModel>> GetUsers() =>                                           // get all
        _db.LoadData<UserModel, dynamic>("select * from user", new { });

    public async Task<UserModel?> GetUser(int id)                                               // get one 
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "select * from user where id = @id",
            new { id });

        return results.FirstOrDefault(); // return first record or null - UserModel? 
    }
    public Task InsertUser(UserModel user) =>                                                   //insert one
        _db.SaveData("insert into user (first_name, last_name) values (@first_name, @last_name)", new { user.first_name, user.last_name});

    public Task UpdateUser(UserModel user) =>                                                   //update one
        _db.SaveData("update user set first_name = @first_name, last_name = @last_name where id = @id", user);

    public Task DeleteUser(int id) =>
        _db.SaveData("delete from user where id = @id", new { id });
}
