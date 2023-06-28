using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Motion.Database;
using Motion.Models;
using Motion.Models.Domain;
using Motion.Validation;
using System.Data;

namespace Motion.Controllers
{
    public class RiderController : Controller
    {
        private string connectionString;
        public readonly dbApi db;
        public RiderController(dbApi db)
        {
            this.db = db;
            connectionString = "server=ELEUS-IDEAPAD;database=Motion;Trusted_connection=true;TrustServerCertificate=true";
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindRides([FromBody] AddRequestModel model)
        {
            string query = "select id, lat, lng, vehicle from DLocations where status='available'";
            var data = new List<RideViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the column values using the column names or indexes
                            Guid id = reader.GetGuid(reader.GetOrdinal("Id"));
                            double lat = reader.GetDouble(reader.GetOrdinal("lat"));
                            double lon = reader.GetDouble(reader.GetOrdinal("lng"));
                            string vehicle = reader.GetString(reader.GetOrdinal("Vehicle"));
                            var temp = new RideViewModel();
                            temp.Id = id;
                            temp.Vehicle = vehicle;
                            temp.Lat = lat;
                            temp.Lng = lon;
                            data.Add(temp);
                        }   
                    }
                }
            }
            return Json(data);
        }
        [HttpPost]
        public IActionResult SendRequest([FromBody] AddRequestModel model)
        {
            var data = new RRResponse();
            var reqeust = new Request();
            reqeust.RequestId = Guid.NewGuid();
            reqeust.Rid = model.rid;
            reqeust.Did = model.did;
            reqeust.SLatitude = model.SLatitude;
            reqeust.SLongitude = model.SLongitude;
            reqeust.ELatitude = model.ELatitude;
            reqeust.ELongitude = model.ELongitude;
            reqeust.Status = model.Status;
            db.SendRequest(reqeust);
            data.message = "ok";
            return Json(data);
        }
        [HttpPost]
        public IActionResult RequestStatus([FromBody] AddRequestModel model)
        {
            var data = new RRResponse();
            data.message = db.RequestStatus(model.rid);
            if (data.message == "confirmed")
            {
                data.contact = db.DriverContact(model.did);
            }
            return Json(data);
        }

        [HttpPost]
        public IActionResult ClearRequest([FromBody] AddRequestModel model)
        {
            var data = new RRResponse();
            db.clearRequest(model.rid);
            data.message = "ok";
            return Json(data);
        }
    }
}
