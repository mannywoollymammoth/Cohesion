using System.IO;
using System.Text.Json;
using CohesionIB.ApiEngineer.CodeChallenge.Models;

namespace CohesionIB.ApiEngineer.CodeChallenge.DAL
{
    public class DataAccessLayer : IDataAccessLayer
    {
        UserList _userList;
        public DataAccessLayer()
        {
            string fileName = "Data/users.json";
            string json = File.ReadAllText(fileName);
            _userList = JsonSerializer.Deserialize<UserList>(json);
        }
        public UserList getUserList()
        {
            return _userList;
        }
        public bool SaveChanges()
        {
            string fileName = "Data/users.json";
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(_userList, options);
            File.WriteAllText(fileName, json);
            return true;
        }

    }
}