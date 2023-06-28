using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Motion.Models;
using Motion.Models.Domain;
using Motion.Validation;
using Motion.Database;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Motion.Controllers
{
    public class RegisterController : Controller
    {
        public readonly dbApi db;
        public RegisterController(dbApi db)
        {
            this.db = db;
        }
        public IActionResult RiderRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RiderRegister([FromBody] AddRiderViewModel model)
        {
            var rider = new Rider();
            var response = new RRResponse();
            if (model.Name.IsNullOrEmpty() || model.Password.IsNullOrEmpty() || model.Contact.IsNullOrEmpty()) {
                response.message = "empty";
                return Json(response);
            }
            rider.Name = model.Name;
            rider.Password = model.Password;
            rider.Id = Guid.NewGuid();
            var checker = new Validator();
            if (checker.IsValidEmail(model.Contact))
            {
                rider.Email = model.Contact;
                rider.PhoneNumber = null;
                if (db.ExistEmail("Drivers", rider.Email) || db.ExistEmail("Riders", rider.Email))
                {
                    response.message = "exist";
                    return Json(response);
                }
            }
            else if (checker.IsPhoneNumber(model.Contact)) 
            { 
                rider.PhoneNumber = model.Contact;
                rider.Email = null;
                if (db.ExistPhone("Drivers", rider.PhoneNumber) || db.ExistPhone("Riders", rider.PhoneNumber))
                {
                    response.message = "exist";
                    return Json(response);
                }
            }
            else
            {
                response.message = "invalid";
                return Json(response);
            }
            response.message = "ok";
            db.createRider(rider);
            return Json(response);
        }
        [HttpGet]
        public IActionResult DriverRegister() { return View(); }
        [HttpPost]
        public async Task<IActionResult> DriverRegister([FromBody] AddDriverViewModel model) 
        {
            bool valid = true;
            var response = new RRResponse();
            if (model.Name.IsNullOrEmpty()) valid = false;
            if (model.Password.IsNullOrEmpty()) valid = false;
            if (model.Email.IsNullOrEmpty()) valid = false;
            if (model.License_plate.IsNullOrEmpty()) valid = false;
            if (model.Phone_Number.IsNullOrEmpty()) valid = false;
            if (model.Vehicle_type.IsNullOrEmpty()) valid = false;
            if (!valid)
            {
                response.message = "empty";
                return Json(response);
            }
            var checker = new Validator();
            if (!checker.IsValidEmail(model.Email))
            {
                response.message = "in_em";
                return Json(response);
            }
            if (!checker.IsPhoneNumber(model.Phone_Number))
            {
                response.message = "in_pn";
                return Json(response);
            }
            var driver = new Driver();
            driver.Id = Guid.NewGuid();
            driver.Name = model.Name;
            driver.Password = model.Password;
            driver.Email = model.Email;
            driver.PhoneNumber = model.Phone_Number;
            driver.Vehicle_type = model.Vehicle_type;
            driver.License_plate = model.License_plate;

            if (db.ExistEmail("Drivers", driver.Email) || db.ExistEmail("Riders", driver.Email))
            {
                response.message = "ex_em";
                return Json(response);
            }
            if (db.ExistPhone("Drivers", driver.PhoneNumber) || db.ExistPhone("Riders", driver.PhoneNumber))
            {
                response.message = "ex_pn";
                return Json(response);
            }
            db.createDriver(driver);
            var dLocation = new DLocation();
            dLocation.Id = driver.Id;
            dLocation.Vehicle = driver.Vehicle_type;
            dLocation.Status = "available";
            dLocation.lat = 23.73546;
            dLocation.lng = 90.40037;
            db.insertDLocation(dLocation);

            response.message = "ok";
            return Json(response);
        }
    }
}
