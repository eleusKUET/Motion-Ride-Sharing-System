using System.Text.RegularExpressions;

namespace Motion.Validation
{
    public class Validator
    {
        public Validator() { }
        public Boolean IsPhoneNumber(String phoneNumber)
        {
            string pattern = @"^\+8801\d{9}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }
        public Boolean IsValidEmail(String email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
