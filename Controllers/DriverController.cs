using Microsoft.AspNetCore.Mvc;
using Motion.Models;
using Motion.Models.Domain;
using Motion.Validation;

namespace Motion.Controllers
{
    public class DriverController : Controller
    {
        public readonly dbApi db;
        public DriverController(dbApi db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PositionUpdate([FromBody] DriverLocationViewModel location)
        {
            db.updateLocation(location);
            var response = new RRResponse();
            response.message = "ok";
            return Json(response);
        }
        [HttpPost]
        public IActionResult RequestList([FromBody] DriverRequestModel model)
        {
            Request req = db.RideRequest(model);
            /*if (req == null)
            {
                req = new Request();
                req.Did = model.DriverId;
                req.Rid = model.DriverId;
            }*/
            return Json(req);
        }
        [HttpPost]
        public IActionResult ConfirmRequest([FromBody] DriverRequestModel model)
        {
            var response = new RRResponse();
            response.message = "request confirmed";
            db.ConfirmRequest(model.DriverId);
            return Json(response);
        }
        [HttpPost]
        public IActionResult CencelRequest([FromBody] DriverRequestModel model)
        {
            var response = new RRResponse();
            response.message = "request cenceled";
            db.CencelRequest(model.DriverId);
            return Json(response);
        }
    }
}
