using MegaMindsTechProject.BLL;
using MegaMindsTechProject.BLL.Interface;
using MegaMindsTechProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace MegaMindsTechProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Invalid data" });

                int result;

                if (model.Id > 0)
                    result = await _user.UpdateUser(model);
                else
                    result = await _user.InsertUser(model);

                return Json(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var data = await _user.GetUsers();
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _user.GetUserById(id);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _user.DeleteUser(id);
                return Json(new { success = result > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
