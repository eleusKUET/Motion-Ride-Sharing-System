using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Motion.Database;
using Motion.Models;
using Motion.Validation;
using Microsoft.EntityFrameworkCore;

namespace Motion.Controllers
{
    public class LoginController : Controller
    {
        public readonly dbApi db;
        public LoginController(dbApi db) 
        {   
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin([FromBody] UserLoginViewModel model)
        {
            var response = new RRResponse();
            if (model.Contact.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            {
                response.message = "empty";
                return Json(response);
            }
            var checker = new Validator();
            if (!checker.IsValidEmail(model.Contact) && !checker.IsPhoneNumber(model.Contact))
            {
                response.message = "invalid";
                return Json(response);
            }
            if (checker.IsValidEmail(model.Contact))
            {
                if (db.Login("Drivers", "email", model.Contact, model.Password) != Guid.Empty)
                {
                    response.message = "driver";
                    var driver = db.FindDriver("select * from drivers where email='" + model.Contact + "'");
                    response.userid = driver.Id.ToString();
                    response.username = driver.Name;
                    response.usertype = "driver";
                    return Json(response);
                }
                if (db.Login("Riders", "email", model.Contact, model.Password) != Guid.Empty)
                {
                    response.message = "rider";
                    var rider = db.FindRider("select * from riders where email='" + model.Contact + "'");
                    response.userid = rider.Id.ToString();
                    response.username = rider.Name;
                    response.usertype = "rider";
                    return Json(response);
                }
            }
            if (checker.IsPhoneNumber(model.Contact))
            {
                if (db.Login("Drivers", "phonenumber", model.Contact, model.Password) != Guid.Empty)
                {
                    response.message = "driver";
                    var driver = db.FindDriver("select * from drivers where phonenumber='" + model.Contact + "'");
                    response.userid = driver.Id.ToString();
                    response.username = driver.Name;
                    response.usertype = "driver";
                    return Json(response);
                }
                if (db.Login("Riders", "phonenumber", model.Contact, model.Password) != Guid.Empty)
                {
                    response.message = "rider";
                    var rider = db.FindRider("select * from riders where phonenumber='" + model.Contact + "'");
                    response.userid = rider.Id.ToString();
                    response.username = rider.Name;
                    response.usertype = "rider";
                    return Json(response);
                }
            }
            response.message = "not_ex";
            return Json(response);
        }
    }
}
