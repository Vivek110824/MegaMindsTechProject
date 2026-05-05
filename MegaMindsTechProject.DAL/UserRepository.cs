using MegaMindsTechProject.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MegaMindsTechProject.DAL
{
    public class UserRepository
    {
        public async Task<int> InsertUser(UserModel model)
        {
            try
            {
                using (var con = new SqlConnection(CommonHelper.ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_InsertUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", model.Name);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Phone", model.Phone);
                        cmd.Parameters.AddWithValue("@Address", model.Address);
                        cmd.Parameters.AddWithValue("@StateId", model.StateId);
                        cmd.Parameters.AddWithValue("@CityId", model.CityId);

                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var list = new List<UserModel>();

            try
            {
                using (var con = new SqlConnection(CommonHelper.ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_GetUsers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await con.OpenAsync();
                        using var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            list.Add(new UserModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Phone = reader["Phone"]?.ToString()
                            });
                        }
                    }
                }

                return list;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = new UserModel();

            try
            {
                using (var con = new SqlConnection(CommonHelper.ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_GetUserById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        await con.OpenAsync();
                        using var reader = await cmd.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.Name = reader["Name"]?.ToString();
                            user.Email = reader["Email"]?.ToString();
                            user.Phone = reader["Phone"]?.ToString();
                            user.Address = reader["Address"]?.ToString();
                            user.StateId = Convert.ToInt32(reader["StateId"]);
                            user.CityId = Convert.ToInt32(reader["CityId"]);
                        }
                    }
                }

                return user;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateUser(UserModel model)
        {
            try
            {
                using (var con = new SqlConnection(CommonHelper.ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_UpdateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@Name", model.Name);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Phone", model.Phone);
                        cmd.Parameters.AddWithValue("@Address", model.Address);
                        cmd.Parameters.AddWithValue("@StateId", model.StateId);
                        cmd.Parameters.AddWithValue("@CityId", model.CityId);

                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            try
            {
                using (var con = new SqlConnection(CommonHelper.ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_DeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        await con.OpenAsync();
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
