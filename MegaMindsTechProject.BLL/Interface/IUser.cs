using MegaMindsTechProject.Model;

namespace MegaMindsTechProject.BLL.Interface
{
    public interface IUser
    {
        Task<int> InsertUser(UserModel model);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUserById(int id);
        Task<int> UpdateUser(UserModel model);
        Task<int> DeleteUser(int id);
    }
}
