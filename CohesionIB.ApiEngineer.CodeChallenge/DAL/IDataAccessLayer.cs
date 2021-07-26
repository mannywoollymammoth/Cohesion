using CohesionIB.ApiEngineer.CodeChallenge.Models;

namespace CohesionIB.ApiEngineer.CodeChallenge.DAL
{
    public interface IDataAccessLayer
    {
        UserList getUserList();
        bool SaveChanges();
    }
}