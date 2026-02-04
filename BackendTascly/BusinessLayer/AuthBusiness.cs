using BackendTascly.Data.ModelsDto.UsersDtos;
using System;

namespace BackendTascly.BusinessLayer
{
    public static class AuthBusiness
    {
        public static (bool, string) IsPostUserDtoValid(PostUserDto request)
        {
            if (request == null)
                return (false, "Passed info is empty");

            return request switch
            {
                var r when !IsEmailValid(r.Username) =>
                    (false, "Email is not valid"),

                var r when !IsPasswordValid(r.Password) =>
                    (false, "Password must be at least 8 characters long, contain at least one uppercase letter and one digit"),

                var r when string.IsNullOrEmpty(r.FirstName) || string.IsNullOrEmpty(r.LastName) =>
                    (false, "First name and last name cannot be empty"),

                var r when string.IsNullOrEmpty(r.OrganizationName) =>
                    (false, "Organization name cannot be empty"),

                _ => (true, string.Empty)
            };
        }

        public static bool IsEmailValid(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPasswordValid(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            var hasMinimum8Chars = password.Length >= 8;
            var hasUpperCase = password.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()) >= 0;
            var hasDigit = password.IndexOfAny("0123456789".ToCharArray()) >= 0;

            return hasMinimum8Chars && hasUpperCase && hasDigit;
        }
    }
}
