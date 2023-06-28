using Azure.Core;
using Microsoft.Data.SqlClient;
using Motion.Database;
using Motion.Models;
using Motion.Models.Domain;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Request = Motion.Models.Domain.Request;

namespace Motion.Validation
{
    public class dbApi
    {
        public readonly MotionDBContext dbContext;
        public string Connection;
        public dbApi(MotionDBContext dbContext)
        {
            this.dbContext = dbContext;
            Connection = "server=ELEUS-IDEAPAD;database=Motion;Trusted_connection=true;TrustServerCertificate=true";
        }
        public void createRider(Rider rider)
        {
            dbContext.Riders.Add(rider);
            dbContext.SaveChanges();
        }
        public void createDriver(Driver driver)
        {
            dbContext.Drivers.Add(driver);
            dbContext.SaveChanges();
        }
        public bool ExistEmail(string tableName, string email)
        {
            string query = "select id from " + tableName + " where email='"+email+"'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            connection.Close();
                            return true;
                        }
                    }
                    connection.Close();
                }
            }
            return false;
        }
        public bool ExistPhone(string tableName, string phone)
        {
            string query = "select id from " + tableName + " where phonenumber='" + phone + "'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            connection.Close();
                            return true;
                        }
                    }
                    connection.Close();
                }
            }
            return false;
        }
        public bool ExistPassword(string tableName, string password)
        {
            string query = "select id from " + tableName + " where password='" + password + "'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            connection.Close();
                            return true;
                        }
                    }
                    connection.Close();
                }
            }
            return false;
        }
        public Guid GetIdByEmail(string tableName, string email)
        {
            string query = "select id from " + tableName + " where email='" + email + "'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetGuid(reader.GetOrdinal("id"));
                        }
                    }
                }
            }
            return Guid.Empty;
        }
        public Guid GetIdByPhone(string tableName, string phone)
        {
            string query = "select id from " + tableName + " where phonenumber='" + phone + "'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetGuid(reader.GetOrdinal("id"));
                        }
                    }
                }
            }
            return Guid.Empty;
        }
        public Guid Login(string tableName, string col, string contact, string password)
        {
            string query = "select id from " + tableName + " where " + col + "='" + contact + "' and password='" + password + "'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetGuid(reader.GetOrdinal("id"));
                        }
                    }
                }
            }
            return Guid.Empty;
        }
        public Driver FindDriver(string query)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var driver = new Driver();
                            driver.Id = reader.GetGuid(reader.GetOrdinal("id"));
                            driver.Name = reader.GetString(reader.GetOrdinal("name"));
                            driver.Email = reader.GetString(reader.GetOrdinal("email"));
                            driver.PhoneNumber = reader.GetString(reader.GetOrdinal("phonenumber"));
                            driver.Vehicle_type = reader.GetString(reader.GetOrdinal("vehicle_type"));
                            driver.License_plate = reader.GetString(reader.GetOrdinal("License_plate"));
                            driver.Password = reader.GetString(reader.GetOrdinal("password"));
                            return driver;
                        }
                    }
                }
            }
            return null;
        }
        public Rider FindRider(string query)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rider = new Rider();
                            rider.Id = reader.GetGuid(reader.GetOrdinal("id"));
                            rider.Name = reader.GetString(reader.GetOrdinal("name"));
                            rider.Password = reader.GetString(reader.GetOrdinal("password"));
                            return rider;
                        }
                    }
                }
            }
            return null;
        }
        public void insertDLocation(DLocation dLocation)
        {
            dbContext.DLocations.Add(dLocation);
            dbContext.SaveChanges();
        }
        public void updateLocation(DriverLocationViewModel dLocation)
        {
            var dlocation = dbContext.DLocations.Find(dLocation.DriverId);
            dlocation.lat = dLocation.lat;
            dlocation.lng = dLocation.lon;
            dbContext.SaveChanges();
        }
        public void updateDriverStatus(DLocation dLocation, string status)
        {
            string updateQuery = "UPDATE dLocations SET status = @Value1 WHERE id = @Condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@Value1", status);

                    command.Parameters.AddWithValue("@Condition", dLocation.Id);

                    // Execute the update query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public string findVehicle(Guid DriverId)
        {
            string query = "select Vehicle_type from drivers where id=@condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@condition", DriverId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(reader.GetOrdinal("vehicle_type"));
                        }
                    }
                }
            }
            return null;
        }
        public Request RideRequest(DriverRequestModel driverRequestModel)
        {
           
            string query = "select * from requests where did=@condition and status='available'";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@condition", driverRequestModel.DriverId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var request = new Models.Domain.Request();
                            request.Did = reader.GetGuid(reader.GetOrdinal("Did"));
                            request.Rid = reader.GetGuid(reader.GetOrdinal("Rid"));
                            request.SLatitude = reader.GetFloat(reader.GetOrdinal("SLatitude"));
                            request.SLongitude = reader.GetFloat(reader.GetOrdinal("SLongitude"));
                            request.ELongitude = reader.GetFloat(reader.GetOrdinal("ELongitude"));
                            request.ELatitude = reader.GetFloat(reader.GetOrdinal("ELatitude"));
                            request.Status = findVehicle(request.Did);
                            return request;
                        }
                    }
                }
            }
            return null;
        }
        public void SendRequest(Request request)
        {
            dbContext.Requests.Add(request);
            dbContext.SaveChanges();
        }
        public void ConfirmRequest(Guid did)
        {
            string updateQuery = "UPDATE requests SET status = @Value1 WHERE did = @Condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@Value1", "confirmed");

                    command.Parameters.AddWithValue("@Condition", did);

                    // Execute the update query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void CencelRequest(Guid did) {
            string updateQuery = "UPDATE requests SET status = @Value1 WHERE did = @Condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@Value1", "cenceled");

                    command.Parameters.AddWithValue("@Condition", did);

                    // Execute the update query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public String RequestStatus(Guid RiderId)
        {
            string query = "select * from requests where rid=@condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@condition", RiderId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Status = reader.GetString(reader.GetOrdinal("status"));
                            return Status;
                        }
                    }
                }
            }
            return null;
        }
        public void clearRequest(Guid rid)
        {
            string updateQuery = "delete from requests WHERE rid = @Condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Condition", rid);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
        public string DriverContact(Guid did)
        {
            string query = "select * from drivers where id=@condition";
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@condition", did);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(reader.GetOrdinal("phonenumber"));
                        }
                    }
                }
            }
            return null;
        }
    }
}
