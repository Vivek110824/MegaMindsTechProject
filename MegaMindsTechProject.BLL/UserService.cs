using MegaMindsTechProject.BLL.Interface;
using MegaMindsTechProject.DAL;
using MegaMindsTechProject.Model;

namespace MegaMindsTechProject.BLL
{
    public class UserService : IUser
    {
        private readonly UserRepository _repo = new UserRepository();

        public async Task<int> InsertUser(UserModel model)
        {
            return await _repo.InsertUser(model);
        }
        public async Task<List<UserModel>> GetUsers()
        {
            return await _repo.GetUsers();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _repo.GetUserById(id);
        }

        public async Task<int> UpdateUser(UserModel model)
        {
            return await _repo.UpdateUser(model);
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _repo.DeleteUser(id);
        }
    }
}

