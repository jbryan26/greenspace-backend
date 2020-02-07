using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;

namespace TodoApi.Auth
{
    public class TokenProvider
    {


        //Using hard coded values in claims collection list as Data Store for demo. 
        //In reality, User data comes from Database or other Data Source.
        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>
            {

               // new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("AccessLevel", "CompanyAdmin")

            };
            return claims;
        }

        public string LoginUser(string UserID, string Password, User user)
        {

            //Authenticate User, Check if it’s a registered user in Database
            if (user == null)
                return null;

            //If it's registered user, check user password stored in Database 
            //For demo, password is not hashed. Simple string comparison 
            //In real, password would be hashed and stored in DB. Before comparing, hash the password
            if (CryptographyProcessor.Verify(Password, user.PasswordHash))
            {
                //Authentication successful, Issue Token with user credentials
                //Provide the security key which was given in the JWToken configuration in Startup.cs
                var key = Encoding.ASCII.GetBytes
                    ("YourKey-123-askasdaskdkqweqxzmczxckasd");
                //Generate Token for user 
                var jwToken = new JwtSecurityToken(
                    issuer: "http://localhost:5001/",
                    audience: "http://localhost:5001/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
